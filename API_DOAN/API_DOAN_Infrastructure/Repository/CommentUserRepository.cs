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
    public class CommentUserRepository : BaseRepository<CommentUser>, ICommentUserRepository, IDisposable
    {
        public CommentUserRepository(IDOANDbContext dbContext) : base(dbContext) { }


        public IEnumerable<CommentUser> GetThreads(Guid ThreadsId)
        {
            var sql = "SELECT * FROM commentuser c WHERE c.ThreadsId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", ThreadsId);

            var res = _dbContext.Connection.Query<CommentUser>(sql, parameters, transaction: _dbContext.Transaction);
            return res;

        }
        //public bool CheckDuplicateCode(string code)
        //{
        //    var sqlCheck = "SELECT CommentCode FROM Comment c WHERE c.CommentCode = @code";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@code", code);

        //    var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
        //    return res != null;
        //}
    }
}
