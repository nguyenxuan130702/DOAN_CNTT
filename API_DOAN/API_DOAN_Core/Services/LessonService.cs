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
    public class LessonService : BaseService<Lesson>, ILessonService
    {
        ILessonRepository _lesonRepository; 
        public LessonService(ILessonRepository lesonRepository) : base(lesonRepository) 
        {
            _lesonRepository = lesonRepository;
        }
        protected override void ValidateObject(Lesson entity)
        {
            var isDuplicate = _lesonRepository.CheckDuplicateCode(entity.LessonCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_LessonCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _lesonRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_LessonCode_No);
            }
        }
    }
}
