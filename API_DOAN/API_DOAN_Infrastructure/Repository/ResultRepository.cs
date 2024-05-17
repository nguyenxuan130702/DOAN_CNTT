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
    public class ResultRepository : BaseRepository<Result>, IResultRepository, IDisposable
    {
        public ResultRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT ScoreCode FROM Result c WHERE c.ResultCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public Result GetByAss(Guid assignmentId)
        {
            var sqlCheck = "SELECT * FROM Result c WHERE c.AssignmentId = @code1";
            var parameters = new DynamicParameters();
            parameters.Add("@code1", assignmentId);

            var res = _dbContext.Connection.QueryFirstOrDefault<Result>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public Result GetByUser(Guid userId, Guid assignmentId)
        {
            var sqlCheck = "SELECT * FROM Result c WHERE c.UserId = @code AND c.AssignmentId = @code1";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);
            parameters.Add("@code1", assignmentId); 

            var res = _dbContext.Connection.QueryFirstOrDefault<Result>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
