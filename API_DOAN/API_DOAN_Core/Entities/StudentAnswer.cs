using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class StudentAnswer
    {
        public Guid StudentAnswerId { get; set; }
        public string StudentAnswerCode { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public string Checked { get; set; }

    }
}
