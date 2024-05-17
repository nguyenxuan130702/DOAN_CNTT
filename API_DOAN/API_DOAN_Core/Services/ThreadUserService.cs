using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class ThreadUserService : BaseService<ThreadUser>, IThreadUserService
    {
        IThreadUserRepository _threadUserRepository;
        public ThreadUserService(IThreadUserRepository threadRepository) : base(threadRepository)
        {
            _threadUserRepository = threadRepository;
        }
    }
}
