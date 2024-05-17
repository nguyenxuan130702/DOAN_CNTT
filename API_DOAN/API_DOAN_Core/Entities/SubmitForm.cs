using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class SubmitForm
    {
        public Guid SubmitId { get; set; }
        public string SubmitCode { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid UserId { get; set; }
        public IFormFile File { get; set; }
        public DateTime Date { get; set; }
    }
}
