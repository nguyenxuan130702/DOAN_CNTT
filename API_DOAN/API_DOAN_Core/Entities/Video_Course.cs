using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Video_Course 
    {
        public Guid Video_CourseId { get; set; }
        public string Video_CourseCode { get; set; }
        public string Video_CourseTitle { get; set; }
        public string Video_CourseImage { get; set; }
        public string Video_CourseDescription { get; set; }
        public DateTime Created_at { get; set; }

    }
}
