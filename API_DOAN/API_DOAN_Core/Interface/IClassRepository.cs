using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IClassRepository : IBaseRepository<Class>
    {
        bool CheckDuplicateCode(string code);
        IEnumerable<Class> GetByTeacher_Course(Guid teacher_CourseId); 
    }
}
