using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ICommentUserRepository : IBaseRepository<CommentUser>
    {
        //Lấy danh sách lesson theo khóa học. 
        public IEnumerable<CommentUser> GetThreads(Guid ThreadsId);
    }
}
