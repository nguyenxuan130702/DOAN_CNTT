using API_DOAN_Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        [Required(ErrorMessage = DOANConst.ERROR_CourseCode)]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_CourseName)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_CourseContent)]
        public string CourseContent { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_CourseType)]
        public int CourseType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set;}
    }
}
