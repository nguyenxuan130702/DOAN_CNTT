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
    public class Class_AssignmentRepository : BaseRepository<class_assignment>, IViewClassAssignmentRepository, IDisposable
    {
        public Class_AssignmentRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public IEnumerable<class_assignment> GetByTeacher_Course(Guid teacher_CourseId)
        {
            var sqlCheck = "SELECT * FROM class_assignment c WHERE c.Teacher_CourseId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", teacher_CourseId);

            var res = _dbContext.Connection.Query<class_assignment>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
