using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public class GenericRepository<T> : IRepository<T>, IDisposable where T: class
    {
        private readonly DbContext _dataContext;
        private DbSet<T> _dbSet;
        private readonly int _defaultHoursFromNow = 12;

        public GenericRepository()
        {}

        public GenericRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public virtual DbContext DataContext
        {
            get
            {
                return _dataContext;
            }            
        }

        public int ExecuteCommand(string cmdText)
        {
            return 0;
        }

        #region << IRepository<T> members >>

        public T FindById(int key)
        {
            return _dbSet.Find(key);  //_dataContext.Set<T>().Find(key);
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public List<T> FindByKeyword(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public int Delete(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByCriteria(Criteria criteria)
        {
            throw new NotImplementedException();            
        }

        #endregion

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
