using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Video_Question
    {
        public Guid Video_QuestionId { get; set; }
        public string Video_QuestionCode { get; set; }
        public string Video_QuestionContent { get; set; }
        public string? Video_QuestionImage { get; set; }
        public string Video_QuestionResult { get; set; }
        public string Video_QuestionAnswerA { get; set; }
        public string Video_QuestionAnswerB { get; set; }
        public string Video_QuestionAnswerC { get; set; }
        public string Video_QuestionAnswerD { get; set; }
        public Guid VideoId { get; set; }
        public string? Video_QuestionSuggest { get; set; }
    }
}
