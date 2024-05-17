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
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository, IDisposable
    {
        public EnrollmentRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT EnrollmentCode FROM Enrollment c WHERE c.EnrollmentCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public Enrollment GetByUserIdAndTeacher_CourseId(Guid userId, Guid teacher_courseId)
        {
            var sqlCheck = "SELECT * FROM Enrollment c WHERE c.UserId = @code1 AND c.Teacher_CourseId = @code2";
            var parameters = new DynamicParameters();
            parameters.Add("@code1", userId);
            parameters.Add("@code2", teacher_courseId); 

            var res = _dbContext.Connection.QueryFirstOrDefault<Enrollment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res; 
        }
    }
}
