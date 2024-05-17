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
    public class QuestionImage
    {
        public Guid QuestionId { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_QuestionCode)]
        public string QuestionCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_QuestionContent)]
        public string QuestionContent { get; set; }
        
        //public string? QuestionSuggest { get; set; }
        public string? QuestionResult { get; set; }

        public string? AnswerA { get; set; }
        public string? AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string? AnswerD { get; set; }
        //public string? ResultImage { get; set; }
        public Guid? LessonId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        
        public string? Checked { get; set; }

        public IFormFile? ImageQuestion { get; set; }
        public IFormFile? QuestionSuggest { get; set; }
        public IFormFile? ResultImage { get; set; }
    }
}
