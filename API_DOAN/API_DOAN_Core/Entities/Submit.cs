using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Submit
    {
        public Guid SubmitId { get; set; }
        public string SubmitCode { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid UserId { get; set; }
        public string SubmitLink { get; set; }
        public DateTime Date {  get; set; }
    }
}
