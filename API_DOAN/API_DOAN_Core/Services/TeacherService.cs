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
    public class TeacherService : BaseService<Teacher>, ITeacherService
    {
        ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository threadRepository) : base(threadRepository)
        {
            _teacherRepository = threadRepository;
        }
        protected override void ValidateObject(Teacher entity)
        {
            var isDuplicate = _teacherRepository.CheckDuplicateValidate(entity.TeacherCode);
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
