using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class LessonCourse
    {
        public Guid LessonId { get; set; }
        public string LessonCode { get; set; }
        public string LessonName { get; set;}
        public string LessonContent { get; set; }
        public string CourseName { get; set; }
        public string LessonPdf { get; set; }
    }
}
