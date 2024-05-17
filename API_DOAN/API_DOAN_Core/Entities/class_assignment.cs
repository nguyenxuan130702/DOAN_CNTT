using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class class_assignment
    {
        public Guid ClassId { get; set; }
        public string ClassCode { get; set; }
        public Guid Teacher_CourseId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string ClassLink { get; set; }
        public string DocumentLink { get; set; }
        public string ClassName { get; set; }

        public Guid AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentLink { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}
