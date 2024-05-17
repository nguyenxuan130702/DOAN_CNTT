using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class ThreadsRepository : BaseRepository<Threads>, IThreadsRepository, IDisposable
    {
        public ThreadsRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT ThreadsCode FROM Threads c WHERE c.ThreadsCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Threads> GetThread(Guid threadId)
        {
            var sql = "SELECT * FROM Threads c WHERE c.ThreadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", threadId);

            var res = _dbContext.Connection.Query<Threads>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<Threads> GetCategory(Guid categoryId)
        {
            var sql = "SELECT * FROM Threads c WHERE c.CategoryId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", categoryId);

            var res = _dbContext.Connection.Query<Threads>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<Threads> GetUser(Guid userId)
        {
            var sql = "SELECT * FROM Threads c WHERE c.UserId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);

            var res = _dbContext.Connection.Query<Threads>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
