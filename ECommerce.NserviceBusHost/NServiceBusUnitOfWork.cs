using System;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Linq;
using ECommerce.Storage.Contexts;
using ECommerce.Storage.Repository;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.NHibernate;
using NServiceBus.UnitOfWork;
using ECommerce.Domain;

namespace ECommerce.NserviceBusHost
{
    public class NServiceBusUnitOfWork : IManageUnitsOfWork
    {
        private readonly ILog _logger;
        private readonly NHibernateStorageContext _nHibernateStorageContext;
        private readonly IRepository _repository;
        private readonly IBus _bus;
        public NServiceBusUnitOfWork(ILog logger, NHibernateStorageContext nHibernateStorageContext, IRepository repository, IBus bus)
        {
            _logger = logger;
            _nHibernateStorageContext = nHibernateStorageContext;
            _repository = repository;
            _bus = bus;
        }

        public void Begin()
        {
            _repository.UseExistingUnitOfWork(new Lazy<IUnitOfWork>
                (
                    () =>
                        new EmpContext((DbConnection)_nHibernateStorageContext.Connection))
                );
        }

        public void End(Exception ex = null)
        {
            var headers = _bus.CurrentMessageContext.Headers;
            var dbEx = ex as DbEntityValidationException;
            if (dbEx != null)
            {
                _logger.Error("NServiceBus unhandled exception", ex);
            }
            if (ex != null)
            {
                _logger.Error("NServiceBus unhandled exception", ex);
                return;
            }

            CommitChanges();
        }
        private void CommitChanges()
        {
            try
            {
                _repository.CommitChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                    _logger.Error(String.Format("EF Validation failed: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                throw;
            }
            catch (Exception exc)
            {
                _logger.Error("Commit failed:" + exc.StackTrace, exc);
                throw;
            }
        }
    }
}
