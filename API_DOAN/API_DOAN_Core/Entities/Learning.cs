using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Learning
    {
        public Guid LearningId { get; set; }
        public string LearningCode { get; set; }
        public Guid UserId { get; set; }
        public int LearningAnswer { get; set; }
        public Guid VideoId { get; set; }
        public DateTime Created_at { get; set; }
    }
}
