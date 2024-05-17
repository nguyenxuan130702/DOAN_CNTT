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
    public class StudentAnswerRepository : BaseRepository<StudentAnswer>, IStudentAnswerRepository, IDisposable
    {
        public StudentAnswerRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string answerCode)
        {
            var sqlCheck = "SELECT StudentAnswerId FROM StudentAnswer c WHERE c.StudentAnswerCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", answerCode);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<StudentAnswer> GetByQuestion(Guid questionId)
        {
            var sqlCheck = "SELECT * FROM StudentAnswer c WHERE c.QuestionId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", questionId);

            var res = _dbContext.Connection.Query<StudentAnswer>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<StudentAnswer> GetByUser(Guid userId)
        {
            var sqlCheck = "SELECT * FROM StudentAnswer c WHERE c.UserId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);

            var res = _dbContext.Connection.Query<StudentAnswer>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res; 
        }
    }
}
