using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class ScoreView
    {
        public Guid ScoreId { get; set; }
        public string ScoreCode { get; set; }
        public Guid UserId { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public Guid LessonId { get; set; }
        public string LessonName { get; set;}
        public double Result { get; set; }
        public DateTime Time { get; set; }
    }
}
