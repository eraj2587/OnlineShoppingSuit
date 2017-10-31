using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ECommerce.Domain;
using ECommerce.Storage.Contexts;
using ECommerce.Storage.Helpers;
using ECommerce.Storage.Infrastructure;
using EntityFramework.MappingAPI.Extensions;

namespace ECommerce.Storage.Repository
{
    public class Repository : IRepository
    {
        readonly Lazy<EmpContext> _contextFunc;
        private Lazy<IUnitOfWork> _context;
        //EmpContext _context;
        
        //public Repository(Func<EmpContext> getContext)
        //{
        //    _contextFunc = new Lazy<EmpContext>(getContext);
        //}


        public Repository(IUnitOfWork context)
        {
            if (!(context is DbContext))
                throw new ArgumentException("Invlid context passed in, the context needs to inherit from DbContext");
            _context = new Lazy<IUnitOfWork>(() => context);
        }

        /// <summary>
        /// Add a new entity to database
        /// </summary>
        public T Add<T>(T entity) where T : class
        {
            return GetDbSet<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            var dbContext = GetDbContext();
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                GetDbSet<T>().Attach(entity);
            }
            GetDbSet<T>().Remove(entity);
        }

        /// <summary>
        /// Load an entity by Id with optional includes
        /// </summary>
        public T Load<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var q = GetDbSet<T>().AsQueryable();

            if (includes != null)
            {
                q = includes.Aggregate(q, (set, inc) => set.Include(inc));
            }

            return q.SingleOrDefault(predicate);
        }

        public QueryResult<TResult> Search<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .SingleOrDefault(handlerType.IsAssignableFrom);

            if (handler == null) return null;
            dynamic q = Activator.CreateInstance(handler, GetDbContext());
            return q.Handle(q);
        }

        public TProjection Project<T, TProjection>(Func<IQueryable<T>, TProjection> query) where T : class
        {
            return query(GetDbSet<T>().AsQueryable());
        }

        void Commit()
        {
            _context.Value.CommitChanges();
        }
        public void CommitChanges()
        {
            Commit();
        }

        public void Dispose()
        {
            _context.Value.Dispose();
        }

        public IQueryable<T> LoadList<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            var  queryable = GetDbSet<T>().AsQueryable();
            if (includes != null)
                queryable = includes.Aggregate(queryable, (set, inc) => set.Include(inc));
            return queryable.Where(predicate).AsQueryable();
        }

        public IQueryable<T> All<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var queryable = GetDbSet<T>().AsQueryable();
            
            if (includes != null)
                queryable = includes.Aggregate(queryable, (set, inc) => set.Include(inc));
            return queryable.AsQueryable();
        }

        public virtual void Delete<T>(object id) where T : class
        {
            T entityToDelete = GetDbSet<T>().Find(id);
            Delete(entityToDelete);
        }


        public virtual void Update<T>(T entityToUpdate) where T : class
        {
            InitilizeContext();
            var dbContext = GetDbContext();
            if (dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                GetDbSet<T>().Attach(entityToUpdate);
            }
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void BulkCopy<TModel>(TModel[] models, SqlBulkCopyOptions bulkCopyOptions = SqlBulkCopyOptions.Default) where TModel : ModelBase
        {
            InitilizeContext();
            if (!(GetDbContext().Database.Connection is SqlConnection))
                throw new InvalidOperationException("Not a supported database server.");

            foreach (var model in models)
            {
                model.CreatedOn = DateTime.UtcNow;
            }

            using (var sql = new SqlBulkCopy((SqlConnection)GetDbContext().Database.Connection, bulkCopyOptions, null))
            {
                var map = GetDbContext().Db(typeof(TModel));
                var properties = map.Properties
                    .Where(x => !x.Computed || !x.IsDiscriminator || !x.IsNavigationProperty)
                    .Select(x => new { x.PropertyName, x.ColumnName })
                    .ToList();

                sql.DestinationTableName = map.TableName;
                properties.ForEach(x =>
                {
                    sql.ColumnMappings.Add(x.PropertyName, x.ColumnName);
                });
                sql.WriteToServer(models.ToDataTable());
                sql.EnableStreaming = true;
            }
        }

        void InitilizeContext()
        {
            if (_context == null)
            _context = new Lazy<IUnitOfWork>();
        }

        DbSet<T> GetDbSet<T>() where T : class
        {
            InitilizeContext();
            return GetDbContext().Set<T>();
        }

        DbContext GetDbContext()
        {
            return (DbContext) _context.Value;
        }

        public void UseExistingUnitOfWork(Lazy<IUnitOfWork> unitOfWork)
        {
            _context = unitOfWork;
        }

    }
}