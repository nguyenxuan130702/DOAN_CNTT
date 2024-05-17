using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ITeacher_CourseRepository : IBaseRepository<Teacher_Course>
    {
        public bool CheckDuplicateValidate(string code);
        public IEnumerable<Teacher_Course> GetByTeacher(Guid teacherId); 
    }
}
