using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class CommentUser
    {
        public Guid CommentId { get; set; }
        public string CommentCode { get; set; }
        public string CommentContent { get; set; }
        public string CommentImage {  get; set; }
        public Guid UserId { get; set; }
        public Guid ThreadsId { get; set; }
        public string ThreadsTitle { get; set; }
        public DateTime Created_at { get; set; }
        public string UserImage {  get; set; }
        public string UserAccout {  get; set; }
    }
}
