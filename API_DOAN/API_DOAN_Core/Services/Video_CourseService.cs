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
    public class Video_CourseService : BaseService<Video_Course>, IVideo_CourseService
    {
        IVideo_CourseRepository _videoRepository;
        public Video_CourseService(IVideo_CourseRepository videoRepository) : base(videoRepository)
        {
            _videoRepository = videoRepository;
        }
        protected override void ValidateObject(Video_Course entity)
        {
            // Thuc hien kiem tra ma nhan vien 
            var isDuplicate = _videoRepository.CheckDuplicateCode(entity.Video_CourseCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoObject);
            }
        }

        protected override void ValidateUpdateDelete(string code)
        {
            //thực hiện kiểm tra mã 
            var isDuplicate = _videoRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoCode);
            }
        }
    }
}
