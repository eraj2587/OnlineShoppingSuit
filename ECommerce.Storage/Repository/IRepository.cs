using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using ECommerce.Domain;
using ECommerce.Storage.Infrastructure;

namespace ECommerce.Storage.Repository
{
    public interface IRepository : IUnitOfWork
    {
        TAggregate Add<TAggregate>(TAggregate aggregate) where TAggregate : class;
        TAggregate Load<TAggregate>(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includes) where TAggregate : class;
        IQueryable<TAggregate> LoadList<TAggregate>(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includes) where TAggregate : class;
        QueryResult<TResult> Search<TResult>(IQuery<TResult> query);
        TProjection Project<TAggregate, TProjection>(Func<IQueryable<TAggregate>, TProjection> query) where TAggregate : class;
        void CommitChanges();
        void Delete<TAggregate>(TAggregate aggregate) where TAggregate : class;
        void Delete<T>(object id) where T : class;
        IQueryable<TAggregate> All<TAggregate>(params Expression<Func<TAggregate, object>>[] includes)
            where TAggregate : class;
        void Update<T>(T entityToUpdate) where T : class;
        void BulkCopy<TModel>(TModel[] models, SqlBulkCopyOptions bulkCopyOptions = SqlBulkCopyOptions.Default) where TModel : ModelBase;
        void UseExistingUnitOfWork(Lazy<IUnitOfWork> unitOfWork);
    }
}