using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IThreadUserRepository : IBaseRepository<ThreadUser>
    {
        public IEnumerable<ThreadUser> GetCategory(Guid categoryId);
        public ThreadUser GetThread(Guid threadsId);
    }
}
