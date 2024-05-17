﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class AssignmentFile
    {
        public Guid AssignmentId { get; set; }
        public string AssignmentCode { get; set; }
        public string AssignmentName { get; set; }
        
        public DateTime? Deadline { get; set; }
        public string? Description { get; set; }
        public Guid ClassId { get; set; }
        public IFormFile File { get; set; }
    }
}
