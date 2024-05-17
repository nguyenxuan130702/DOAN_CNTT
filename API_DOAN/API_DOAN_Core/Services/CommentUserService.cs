using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class CommentUserService : BaseService<CommentUser>, ICommentUserService
    {
        ICommentUserRepository _commentUserRepository;
        public CommentUserService(ICommentUserRepository commentRepository) : base(commentRepository)
        {
            _commentUserRepository = commentRepository;
        }
    }
}
