using System;

namespace ECommerce.Storage.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitChanges();
        void Dispose();
    }
}