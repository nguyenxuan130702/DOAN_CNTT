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
    public class EnrollmentService : BaseService<Enrollment>, IEnrollmentService
    {
        IEnrollmentRepository _courseRepository;
        public EnrollmentService(IEnrollmentRepository courseRepository) : base(courseRepository)
        {
            _courseRepository = courseRepository;
        }
        protected override void ValidateObject(Enrollment entity)
        {
            var isDuplicate = _courseRepository.CheckDuplicateCode(entity.EnrollmentCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _courseRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
