using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class CommentImage
    {
        public Guid CommentId { get; set; }
        public string CommentCode { get; set; }
        public string CommentContent { get; set; }
        public Guid UserId { get; set; }
        public Guid ThreadsId { get; set; }
        public DateTime Created_at { get; set; }
        public IFormFile? Image { get; set; }
    }
}
