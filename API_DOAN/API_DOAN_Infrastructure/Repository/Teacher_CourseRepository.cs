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
    public class Teacher_CourseRepository : BaseRepository<Teacher_Course>, ITeacher_CourseRepository, IDisposable
    {
        public Teacher_CourseRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateValidate(string code)
        {
            var sqlCheck = "SELECT Teacher_CourseCode FROM Teacher_Course c WHERE c.Teacher_CourseCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Teacher_Course> GetByTeacher(Guid teacherId)
        {
            var sqlCheck = "SELECT * FROM Teacher_Course c WHERE c.TeacherId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", teacherId);

            var res = _dbContext.Connection.Query<Teacher_Course>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
