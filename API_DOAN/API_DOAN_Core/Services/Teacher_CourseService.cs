using API_DOAN_Core.Entities;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class Teacher_CourseService : BaseService<Teacher_Course>, ITeacher_CourseService
    {
        ITeacher_CourseRepository _teacherRepository;
        public Teacher_CourseService(ITeacher_CourseRepository threadRepository) : base(threadRepository)
        {
            _teacherRepository = threadRepository;
        }
        protected override void ValidateObject(Teacher_Course entity)
        {
            var isDuplicate = _teacherRepository.CheckDuplicateValidate(entity.Teacher_CourseCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _teacherRepository.CheckDuplicateValidate(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
