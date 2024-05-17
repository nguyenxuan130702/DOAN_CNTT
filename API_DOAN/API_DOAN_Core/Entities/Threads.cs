using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Threads
    {
        public Guid ThreadsId { get; set; }
        public string ThreadsCode { get; set;}
        public string ThreadsTitle { get; set; }
        public string ThreadsContent { get; set; }
        public string? ThreadsImage { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created_at { get; set; }
    }
}
