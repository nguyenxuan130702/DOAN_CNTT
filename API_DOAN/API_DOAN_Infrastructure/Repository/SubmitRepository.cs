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
    public class SubmitRepository : BaseRepository<Submit>, ISubmitRepository, IDisposable
    {
        public SubmitRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT SubmitCode FROM Submit c WHERE c.SubmitCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Submit> GetByAss(Guid assignmentId)
        {
            var sqlCheck = "SELECT * FROM Submit c WHERE c.AssignmentId = @code ";
            var parameters = new DynamicParameters();
            parameters.Add("@code", assignmentId);

            var res = _dbContext.Connection.Query<Submit>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<Submit> GetByUser(Guid userId)
        {
            var sqlCheck = "SELECT * FROM Submit c WHERE c.UserId = @code ORDER BY ABS(DATEDIFF(Date, CURRENT_TIMESTAMP)) ASC ";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);

            var res = _dbContext.Connection.Query<Submit>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<Submit> GetByUserAndAssignment(Guid userId, Guid assignmentId)
        {
            var sqlCheck = "SELECT * FROM Submit c WHERE c.UserId = @code AND c.AssignmentId = @code1 ORDER BY ABS(DATEDIFF(Date, CURRENT_TIMESTAMP)) ASC ";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);
            parameters.Add("@code1", assignmentId); 

            var res = _dbContext.Connection.Query<Submit>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
