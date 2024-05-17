using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class userenrollment
    {
        public Guid EnrollmentId { get; set; }
        public string EnrollmentCode { get; set; }
        public Guid Teacher_CourseId { get; set; }
        public DateTime Date {  get; set; }
        public Guid UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserAccout {  get; set; }
        public string UserEmail { get; set; }
    }
}
