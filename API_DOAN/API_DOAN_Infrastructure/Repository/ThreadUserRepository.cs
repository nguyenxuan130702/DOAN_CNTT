using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class ThreadUserRepository : BaseRepository<ThreadUser>, IThreadUserRepository, IDisposable
    {
        public ThreadUserRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public IEnumerable<ThreadUser> GetCategory(Guid categoryId)
        {
            var sql = "SELECT * FROM ThreadUser c WHERE c.CategoryId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", categoryId);

            var res = _dbContext.Connection.Query<ThreadUser>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public ThreadUser GetThread(Guid threadsId)
        {
            var sql = "SELECT * FROM ThreadUser c WHERE c.threadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", threadsId);

            var res = _dbContext.Connection.QueryFirstOrDefault<ThreadUser>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
