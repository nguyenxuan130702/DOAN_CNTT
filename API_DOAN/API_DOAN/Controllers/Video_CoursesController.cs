using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Repository;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/video_course")]
    [ApiController]
    public class Video_CoursesController : ControllerBase
    {
        IVideo_CourseRepository _videoCourseRepository;
        IVideo_CourseService _videoCourseService;
        private readonly Cloudinary _cloudinary;
        public Video_CoursesController(IVideo_CourseRepository videoCourseRepository, IVideo_CourseService videoCourseService)
        {
            _videoCourseRepository = videoCourseRepository;
            _videoCourseService = videoCourseService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _videoCourseRepository.GetAll();
            return Ok(res); 
        }
        [HttpPost]
        public IActionResult Post(Video_Course v)
        {
            var res = _videoCourseService.InsertService(v);
            return Ok(res); 
        }
        [HttpPut]
        public IActionResult Put(Video_Course v)
        {
            var res = _videoCourseService.UpdateService(v);
            return Ok(res); 
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _videoCourseService.DeleteService(code);
            return Ok(res); 
        }
        //Api upload anh 
        [HttpPost]
        [Route("image_video")]
        [DisableRequestSizeLimit]
        public IActionResult postVideo([FromForm] VideoImage v)
        {
            var check = _videoCourseRepository.CheckDuplicateCode(v.Video_CourseCode);
            if (check)
            {
                return Ok("Ma video da ton tai.");
            }
            else
            {
                var videos = new Video_Course { Video_CourseId = Guid.NewGuid(), Video_CourseCode = v.Video_CourseCode, Video_CourseTitle = v.Video_CourseTitle, Video_CourseDescription = v.Video_CourseDescription, Created_at = v.Created_at};
                if (v.Image.Length > 0)
                {
                    using (var stream = v.Image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.Image.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);
                        string imageUrl = uploadResult.Uri.ToString();

                        videos.Video_CourseImage = imageUrl;
                    }
                }
                var res = _videoCourseService.InsertService(videos);
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
        //Api chinh sua video 
        [HttpPut]
        [Route("update_video")]
        public IActionResult updateVideo([FromForm] VideoImage v)
        {
            var check = _videoCourseRepository.CheckDuplicateCode(v.Video_CourseCode);
            if (!check)
            {
                return Ok("Ma video chua ton tai.");
            }
            else
            {
                var videos = new Video_Course{ Video_CourseId = v.Video_CourseId, Video_CourseCode = v.Video_CourseCode, Video_CourseTitle = v.Video_CourseTitle, Video_CourseDescription = v.Video_CourseDescription, Created_at = v.Created_at};
                if (v.Image.Length > 0)
                {
                    using (var stream = v.Image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(v.Image.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);
                        string imageUrl = uploadResult.Uri.ToString();

                        videos.Video_CourseImage = imageUrl;
                    }
                }
                var res = _videoCourseService.UpdateService(videos);
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
    }
}
