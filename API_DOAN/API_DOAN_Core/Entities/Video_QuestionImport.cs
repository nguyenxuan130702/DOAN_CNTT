using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Video_QuestionImport : Video_Question
    {
        public Video_QuestionImport()
        {
            this.ImportValidateError = new List<string>();
        }
        public List<string> ImportValidateError { get; set; }
        public bool IsImported { get; set; }
    }
}
