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
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository, IDisposable
    {
        public CommentRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT CommentCode FROM Comment c WHERE c.CommentCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Comment> GetThreadsId(Guid threadsId)
        {
            var sqlCheck = "SELECT * FROM Comment c WHERE c.ThreadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", threadsId);

            var res = _dbContext.Connection.Query<Comment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res; 
        }
        public IEnumerable<Comment> GetUser(Guid userId)
        {
            var sqlCheck = "SELECT * FROM Comment c WHERE c.UserId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);

            var res = _dbContext.Connection.Query<Comment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
