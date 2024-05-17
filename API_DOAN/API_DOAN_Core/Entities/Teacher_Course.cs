using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Teacher_Course
    {
        public Guid Teacher_CourseId { get; set; }
        public string Teacher_CourseCode { get; set; }
        public string Teacher_CourseName { get; set; }
        public string Teacher_CourseDescription { get; set; }
        public string Teacher_CourseImage { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? RegistrationStartTime { get; set; }
        public DateTime? RegistrationEndTime { get; set;}

    }
}
