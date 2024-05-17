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
    public class ThreadsService : BaseService<Threads>, IThreadsService
    {
        IThreadsRepository _threadRepository;
        public ThreadsService(IThreadsRepository threadRepository) : base(threadRepository)
        {
            _threadRepository = threadRepository;
        }
        protected override void ValidateObject(Threads entity)
        {
            var isDuplicate = _threadRepository.CheckDuplicateCode(entity.ThreadsCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _threadRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
