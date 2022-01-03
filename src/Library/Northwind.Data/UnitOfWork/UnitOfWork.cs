using Northwind.Data.Context;
using Northwind.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {

        #region Variable

        private readonly NorthwndContext _context;

        private IRepository<Product> _productRepository { get; set; }
        #endregion

        #region ctor
        public UnitOfWork()
        {
            _context = new NorthwndContext();
        }
        #endregion

        #region  Repository
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new RepositoryBase<Product>(_context);
                }
                return _productRepository;
            }
        }
        #endregion

        #region Transactions
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            _context.Database.CurrentTransaction.Rollback();
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion

        #region Disposable
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
