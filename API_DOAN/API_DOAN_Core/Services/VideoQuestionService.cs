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
    public class VideoQuestionService : BaseService<VideoQuestion>, IVideoQuestionService
    {
        IVideoQuestionRepository _videoQuestionRepository;
        public VideoQuestionService(IVideoQuestionRepository videoRepository) : base(videoRepository)
        {
            _videoQuestionRepository = videoRepository;
        }
        protected override void ValidateObject(VideoQuestion entity)
        {
            // Thuc hien kiem tra ma nhan vien 
            var isDuplicate = _videoQuestionRepository.CheckDuplicateCode(entity.Video_QuestionCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoObject);
            }
        }

        protected override void ValidateUpdateDelete(string code)
        {
            //thực hiện kiểm tra mã 
            var isDuplicate = _videoQuestionRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoCode);
            }
        }
    }
}
