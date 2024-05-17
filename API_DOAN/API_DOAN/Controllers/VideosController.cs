using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Repository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/videos")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        IVideoRepository videoRepository;
        IVideoService _videoService;
        private readonly Cloudinary _cloudinary;

        public VideosController(IVideoRepository videoRepository, IVideoService videoService)
        {
            this.videoRepository = videoRepository;
            _videoService = videoService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = videoRepository.GetAll();
            return StatusCode(200, videos); 
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var videos = videoRepository.Get(code); 
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("video_Course")]
        public IActionResult GetCourse(Guid video_CourseId)
        {
            var videos = videoRepository.GetVideo_Course(video_CourseId);
            return StatusCode(200, videos);
        }
        [HttpPost]
        public IActionResult Insert(API_DOAN_Core.Entities.Video video)
        {
            var res = _videoService.InsertService(video); 
            if(res.Success == true)
            {
                return StatusCode(201, res); 
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res); 
            }
        }

        //Thêm mới có link câu hỏi đáp án. 
        [HttpPost]
        [Route("uploadfile")]
        public IActionResult UploadFile([FromForm] VideoFile v)
        {
            var check = videoRepository.CheckDuplicateCode(v.VideoCode);
            if (check)
            {
                return Ok("Mã video đã có trong hệ thống.");
            }
            else
            {
                var video = new API_DOAN_Core.Entities.Video { VideoId = Guid.NewGuid(), VideoCode = v.VideoCode, VideoTitle = v.VideoTitle, VideoLink = v.VideoLink, Video_CourseId = v.Video_CourseId, CreatedDate = v.CreatedDate };
                if (v.File_Question.Length > 0)
                {
                    using (var stream = v.File_Question.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Question.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfQuestion = fileUrl.ToString();
                    }
                }
                else
                {
                    video.VideoPdfQuestion = ""; 
                }

                if(v.File_Answer.Length > 0)
                {
                    using (var stream = v.File_Answer.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Answer.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfAnswer = fileUrl.ToString();
                    }
                }
                else
                {
                    video.VideoPdfAnswer = "";
                }

                var res = _videoService.InsertService(video);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }

        }
        [HttpPut]
        public IActionResult Update(API_DOAN_Core.Entities.Video video)
        {
            var res = _videoService.UpdateService(video);
            if(res.Success == true)
            {
                return StatusCode(200, res); 
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res); 
            }
        }
        [HttpPut]
        [Route("updatefile")]
        public IActionResult updateFile([FromForm] VideoFile v)
        {
            var check = videoRepository.CheckDuplicateCode(v.VideoCode);
            if (!check)
            {
                return Ok("Mã video chưa có trong hệ thống.");
            }
            else
            {
                var video = new API_DOAN_Core.Entities.Video { VideoId = v.VideoId, VideoCode = v.VideoCode, VideoTitle = v.VideoTitle, VideoLink = v.VideoLink, Video_CourseId = v.Video_CourseId, CreatedDate = v.CreatedDate };
                if (v.File_Question.Length > 0)
                {
                    using (var stream = v.File_Question.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Question.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfQuestion = fileUrl.ToString();
                    }
                }

                if (v.File_Answer.Length > 0)
                {
                    using (var stream = v.File_Answer.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Answer.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfAnswer = fileUrl.ToString();
                    }
                }

                var res = _videoService.UpdateService(video);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }
        }
        [HttpPut]
        [Route("update_file_answer")]
        public IActionResult Update_Answer([FromForm] VideoPdfAnswer v)
        {
            var check = videoRepository.CheckDuplicateCode(v.VideoCode);
            if (!check)
            {
                return Ok("Mã video chưa có trong hệ thống.");
            }
            else
            {
                var video = new API_DOAN_Core.Entities.Video { VideoId = v.VideoId, VideoCode = v.VideoCode, VideoTitle = v.VideoTitle, VideoLink = v.VideoLink, Video_CourseId = v.Video_CourseId, CreatedDate = v.CreatedDate, VideoPdfQuestion = v.VideoPdfQuestion };
                if (v.File_Answer.Length > 0)
                {
                    using (var stream = v.File_Answer.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Answer.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfAnswer = fileUrl.ToString();
                    }
                }

                var res = _videoService.UpdateService(video);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }
        }

        [HttpPut]
        [Route("update_file_question")]
        public IActionResult Update_File_Question([FromForm] VideoPdfQuestion v)
        {
            var check = videoRepository.CheckDuplicateCode(v.VideoCode);
            if (!check)
            {
                return Ok("Mã video chưa có trong hệ thống.");
            }
            else
            {
                var video = new API_DOAN_Core.Entities.Video { VideoId = v.VideoId, VideoCode = v.VideoCode, VideoTitle = v.VideoTitle, VideoLink = v.VideoLink, Video_CourseId = v.Video_CourseId, CreatedDate = v.CreatedDate, VideoPdfAnswer = v.VideoPdfAnswer };
                if (v.File_Question.Length > 0)
                {
                    using (var stream = v.File_Question.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.File_Question.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        video.VideoPdfQuestion = fileUrl.ToString();
                    }
                }

                var res = _videoService.UpdateService(video);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _videoService.DeleteService(code); 
            if(res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res); 
            }
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] codes)
        {
            var res = _videoService.DeleteAnyService(codes);
            return Ok(res); 
        }
    }
}
