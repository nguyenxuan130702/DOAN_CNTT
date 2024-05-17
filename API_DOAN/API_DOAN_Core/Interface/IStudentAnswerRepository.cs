using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IStudentAnswerRepository : IBaseRepository<StudentAnswer>
    {
        public IEnumerable<StudentAnswer> GetByUser(Guid userId);
        public IEnumerable<StudentAnswer> GetByQuestion(Guid questionId); 

        public bool CheckDuplicateCode(string answerCode);

    }
}
