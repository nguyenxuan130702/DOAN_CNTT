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
    public class VideoFile
    {
        public Guid VideoId { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_VideoCode)]
        public string VideoCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_VideoTitle)]
        public string VideoTitle { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_VideoLink)]
        public string VideoLink { get; set; }
        public IFormFile? File_Question { get; set; }
        public IFormFile? File_Answer { get; set; }
        public Guid Video_CourseId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
