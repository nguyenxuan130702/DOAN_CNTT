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
    public class CommentService : BaseService<Comment>, ICommentService
    {
        ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository) : base(commentRepository)
        {
            _commentRepository = commentRepository;
        }
        protected override void ValidateObject(Comment entity)
        {
            var isDuplicate = _commentRepository.CheckDuplicateCode(entity.CommentCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _commentRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
