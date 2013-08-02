using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAnalyst.Model;

namespace SupportAnalyst.Repository
{
    public class RepositoryBase : IDisposable
    {
        private DbContext _dataContext;

        public RepositoryBase()
        {}

        public RepositoryBase(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual DbContext DataContext
        {
            get
            {
                return _dataContext;
            }            
        }

        public virtual T FindById<T>(int key) where T : class
        {
            return _dataContext.Set<T>().Find(key);
        }

        public int ExecuteCommand(string cmdText)
        {
            return 0;
        }

        #region << IDisposable >>

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
