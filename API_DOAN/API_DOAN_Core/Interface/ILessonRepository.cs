using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ILessonRepository:IBaseRepository<Lesson>
    {
        public bool CheckDuplicateCode(string code);

        //Lấy danh sách lesson theo khóa học. 
        public IEnumerable<Lesson> GetLessonQuestion(Guid CourseId); 
    }
}
