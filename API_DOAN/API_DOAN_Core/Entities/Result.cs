using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Result
    {
        public Guid ResultId { get; set; }
        public string ResultCode { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid UserId { get; set; }
        public double Score { get; set; }
        public string? Feedback { get; set; }
    }
}
