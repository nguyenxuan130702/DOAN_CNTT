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
    public class SubmitService : BaseService<Submit>, ISubmitService
    {
        ISubmitRepository _teacherRepository;
        public SubmitService(ISubmitRepository threadRepository) : base(threadRepository)
        {
            _teacherRepository = threadRepository;
        }
        protected override void ValidateObject(Submit entity)
        {
            var isDuplicate = _teacherRepository.CheckDuplicateCode(entity.SubmitCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _teacherRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
