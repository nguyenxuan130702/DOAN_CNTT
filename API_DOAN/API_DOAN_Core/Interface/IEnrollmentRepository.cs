using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        bool CheckDuplicateCode(string code);
        Enrollment GetByUserIdAndTeacher_CourseId(Guid userId, Guid teacher_courseId); 
    }
}
