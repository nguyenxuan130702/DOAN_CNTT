using API_DOAN_Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DOAN_Core.Interface; 

namespace API_DOAN_Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T: class
    {
        protected IDOANDbContext _dbContext; 
        public BaseRepository(IDOANDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Delete(string code)
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            var res = _dbContext.Delete<T>(code);
            _dbContext.Transaction.Commit();
            return res;
            //throw new NotImplementedException();
        }

        public int DeleteAny(string[] codes)
        {
            //throw new NotImplementedException();
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            var res = _dbContext.DeleteAny<T>(codes);
            _dbContext.Transaction.Commit();
            return res;
        }

        public void Dispose()
        {
            _dbContext.Connection.Close();
        }

        public T? Get(string code)
        {
            var res = _dbContext.Get<T>(code);
            return res; 
            //throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            var res = _dbContext.Get<T>();
            return res; 
            //throw new NotImplementedException();
        }

        public IEnumerable<T> GetPage(int page, int pageSize)
        {
            var res = _dbContext.Get<T>();
            res = res.Skip((page - 1) * pageSize).Take(pageSize);
            return res; 
        }

        public int Insert(T entity)
        {
            var res = _dbContext.Insert<T>(entity);
            return res; 
            //throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            var res = _dbContext.Update<T>(entity);
            _dbContext.Transaction.Commit();
            return res;
            //throw new NotImplementedException();
        }
    }
}
