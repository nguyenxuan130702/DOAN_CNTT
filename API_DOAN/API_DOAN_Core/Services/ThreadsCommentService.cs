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
    public class ThreadsCommentService : BaseService<ThreadsComment>, IThreadsCommentService
    {
        IThreadsCommentRepository _threadsCommentRepository;
        public ThreadsCommentService(IThreadsCommentRepository threadsCommentRepository) : base(threadsCommentRepository)
        {
            _threadsCommentRepository = threadsCommentRepository;
        }

        protected override void ValidateObject(ThreadsComment entity)
        {
            var isDuplicate = _threadsCommentRepository.CheckDuplicateCode(entity.ThreadsCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_RadioCode);
            }
        }
    }
}
