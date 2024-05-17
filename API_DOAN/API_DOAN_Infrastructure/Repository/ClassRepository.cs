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
    public class ClassRepository : BaseRepository<Class>, IClassRepository, IDisposable
    {
        public ClassRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT ClassCode FROM Class c WHERE c.ClassCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Class> GetByTeacher_Course(Guid teacher_CourseId)
        {
            var sqlCheck = "SELECT * FROM Class c WHERE c.Teacher_CourseId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", teacher_CourseId);

            var res = _dbContext.Connection.Query<Class>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
