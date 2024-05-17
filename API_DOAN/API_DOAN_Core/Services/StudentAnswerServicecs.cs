using API_DOAN_Core.Entities;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class StudentAnswerServicecs : BaseService<StudentAnswer>, IStudentAnswerService
    {
        IStudentAnswerRepository _studentRepository;
        public StudentAnswerServicecs(IStudentAnswerRepository studentRepository) : base(studentRepository)
        {
            _studentRepository = studentRepository;
        }

        protected override void ValidateObject(StudentAnswer entity)
        {
            var isDuplicate = _studentRepository.CheckDuplicateCode(entity.StudentAnswerCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_RadioCode);
            }
        }
    }
}
