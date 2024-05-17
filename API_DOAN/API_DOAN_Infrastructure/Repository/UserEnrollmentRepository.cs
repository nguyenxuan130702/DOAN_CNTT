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
    public class UserEnrollmentRepository : BaseRepository<userenrollment>, IViewUserEnrollmentRepository, IDisposable
    {
        public UserEnrollmentRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public IEnumerable<userenrollment> GetUserEnrollmentByTeacher_Course(Guid teacher_courseId)
        {
            var sqlCheck = "SELECT * FROM userenrollment c WHERE c.Teacher_CourseId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", teacher_courseId);

            var res = _dbContext.Connection.Query<userenrollment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
