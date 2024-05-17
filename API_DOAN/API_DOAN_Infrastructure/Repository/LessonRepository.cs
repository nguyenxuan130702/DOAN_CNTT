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
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository, IDisposable
    {
        public LessonRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT LessonCode FROM Lesson c WHERE c.LessonCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Lesson> GetLessonQuestion(Guid CourseId)
        {
            var sql = "SELECT * FROM Lesson c WHERE c.CourseId = @code"; 
            var parameters = new DynamicParameters();
            parameters.Add("@code", CourseId); 

            var res = _dbContext.Connection.Query<Lesson>(sql, parameters, transaction: _dbContext.Transaction);
            return res;

        }
    }
}
