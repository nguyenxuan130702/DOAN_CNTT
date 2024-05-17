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
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository, IDisposable
    {
        public QuestionRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT QuestionCode FROM Question c WHERE c.QuestionCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }
        public void Dispose()
        {
            _dbContext.Connection.Dispose();
        }

        public IEnumerable<Question> GetByLesson(Guid id)
        {
            var sql = "SELECT * FROM Question c WHERE c.LessonId = @id"; 
            var parameters = new DynamicParameters();
            parameters.Add("@id", id); 

            var res = _dbContext.Connection.Query<Question>(sql, parameters, transaction: _dbContext.Transaction);
            return res; 
        }
    }
}
