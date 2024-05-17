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
    public class ThreadsCommentRepository : BaseRepository<ThreadsComment>, IThreadsCommentRepository, IDisposable
    {
        public ThreadsCommentRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string answerCode)
        {
            var sqlCheck = "SELECT ThreadsId FROM threadscomment c WHERE c.ThreadsCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", answerCode);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<StudentAnswer> GetByUser(Guid ThreadsId)
        {
            var sqlCheck = "SELECT * FROM threadscomment c WHERE c.ThreadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", ThreadsId);

            var res = _dbContext.Connection.Query<StudentAnswer>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<ThreadsComment> GetCategory(Guid categoryId)
        {
            var sql = "SELECT * FROM ThreadsComment c WHERE c.CategoryId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", categoryId);

            var res = _dbContext.Connection.Query<ThreadsComment>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public ThreadsComment GetThread(Guid threadId)
        {
            var sql = "SELECT * FROM ThreadsComment c WHERE c.ThreadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", threadId);

            var res = _dbContext.Connection.QueryFirstOrDefault< ThreadsComment>(sql, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
