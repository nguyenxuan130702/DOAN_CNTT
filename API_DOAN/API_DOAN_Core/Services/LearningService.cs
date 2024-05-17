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
    public class LearningService : BaseService<Learning>, ILearningService
    {
        ILearningRepository _learningRepository;
        public LearningService(ILearningRepository learningRepository) : base(learningRepository)
        {
            _learningRepository = learningRepository;
        }
        protected override void ValidateObject(Learning entity)
        {
            var isDuplicate = _learningRepository.CheckDuplicateCode(entity.LearningCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _learningRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_CourseCode_No);
            }
        }
    }
}
