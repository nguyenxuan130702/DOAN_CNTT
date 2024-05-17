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
    public class AssignmentService : BaseService<Assignment>, IAssignmentService
    {
        IAssignmentRepository _courseRepository;
        public AssignmentService(IAssignmentRepository courseRepository) : base(courseRepository)
        {
            _courseRepository = courseRepository;
        }
        protected override void ValidateObject(Assignment entity)
        {
            var isDuplicate = _courseRepository.CheckDuplicateCode(entity.AssignmentCode);
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
