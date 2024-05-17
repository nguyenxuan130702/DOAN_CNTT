using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class QuestionLesson 
    {
        public Guid QuestionId { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionImage { get; set; }
        public string QuestionResult { get; set; }
        public string QuestionSuggest { get; set; }
        public string ResultImage { get; set; }
        public string AnswerA {  get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string LessonName { get; set; }
        public Guid LessonId { get; set; }
    }
}
