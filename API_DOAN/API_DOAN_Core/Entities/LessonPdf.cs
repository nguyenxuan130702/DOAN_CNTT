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
    public class LessonPdf
    {
        public Guid LessonId { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_LessonCode)]
        public string LessonCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_LessonName)]
        public string LessonName { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_LessonContent)]
        public string LessonContent { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_CourseId)]
        public Guid CourseId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IFormFile File { get; set; }
    }
}
