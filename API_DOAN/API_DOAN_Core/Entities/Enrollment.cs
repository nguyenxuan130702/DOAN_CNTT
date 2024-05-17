using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Enrollment
    {
        public Guid EnrollmentId { get; set; }
        public string EnrollmentCode { get; set; }
        public Guid Teacher_CourseId {  get; set; }
        public Guid UserId { get; set; }
        public DateTime Date {  get; set; }
    }
}
