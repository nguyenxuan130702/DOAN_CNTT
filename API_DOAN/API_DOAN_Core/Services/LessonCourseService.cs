using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class LessonCourseService : BaseService<LessonCourse>, ILessonCourseService
    {
        ILessonCourseRepository _lesoncourseRepository;
        public LessonCourseService(ILessonCourseRepository lesoncourseRepository) : base(lesoncourseRepository)
        {
            _lesoncourseRepository = lesoncourseRepository;
        }
    }
}
