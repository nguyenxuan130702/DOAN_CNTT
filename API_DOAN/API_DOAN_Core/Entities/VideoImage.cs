using API_DOAN_Core.Const;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class VideoImage
    {
        public Guid Video_CourseId { get; set; }
        public string Video_CourseCode { get; set; }
        public string Video_CourseTitle { get; set; }
        public string Video_CourseDescription { get; set; }
        public DateTime Created_at { get; set; }
        public IFormFile Image {  get; set; } 
    }
}
