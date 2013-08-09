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
        //private readonly DbContext _dataContext;
        //private DbSet<T> _dbSet;        

        public GenericRepository()
        {}

        public GenericRepository(DbContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentException("An instance of DbContext is required.");
            }
            DataContext = dataContext;
            DbSet = DataContext.Set<T>();
        }

        protected DbContext DataContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public int ExecuteCommand(string cmdText)
        {
            return DataContext.Database.ExecuteSqlCommand(cmdText);
        }

        public T FindById(int key)
        {
            return DbSet.Find(key);
        }

        public IQueryable<T> Get()
        {
            return DbSet;
        }

        #region << IRepository<T> members >>

        //public List<T> FindByKeyword(string keyword, int pageIndex, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<T> FindByKeyword(string keyword, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(DateTime startTime, DateTime endTime)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<T> FindByCriteria(Criteria criteria)
        //{
        //    throw new NotImplementedException();            
        //}

        #endregion

        #region << IDisposable >>

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
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
