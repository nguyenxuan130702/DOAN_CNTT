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
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository, IDisposable
    {
        public AssignmentRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT AssignmentCode FROM Assignment c WHERE c.AssignmentCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Assignment> GetByClass(Guid classId)
        {
            var sqlCheck = "SELECT * FROM Assignment c WHERE c.ClassId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", classId);

            var res = _dbContext.Connection.Query<Assignment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
