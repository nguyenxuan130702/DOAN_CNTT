using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        ILessonRepository _lessonRepository;
        ILessonService _lessonService;
        private readonly Cloudinary _cloudinary;
        public LessonController(ILessonRepository lessonRepository, ILessonService lessonService) 
        {
            _lessonRepository = lessonRepository;
            _lessonService = lessonService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = _lessonRepository.GetAll();
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var lesson = _lessonRepository.Get(code);
            return StatusCode(200, lesson);
        }
        [HttpGet]
        [Route("course")]
        public IActionResult GetCourseId(Guid id)
        {
            var course = _lessonRepository.GetLessonQuestion(id);
            return StatusCode(200, course);
        }
        
        [HttpPost]
        public IActionResult Insert(Lesson lesson)
        {
            var res = _lessonService.InsertService(lesson);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
        [HttpPost]
        [Route("FilePdf")]
        public IActionResult PostwithPdf([FromForm] LessonPdf l)
        {
            var check = _lessonRepository.CheckDuplicateCode(l.LessonCode);
            if (!check)
            {
                var lesson = new Lesson { LessonId = Guid.NewGuid(), LessonCode = l.LessonCode, LessonName = l.LessonName, LessonContent = l.LessonContent, CourseId = l.CourseId };
                if(l.File.Length > 0)
                {
                    using (var stream = l.File.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(l.File.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;
                        
                        lesson.LessonPdf = fileUrl; 
                    }
                }
                else
                {
                    lesson.LessonPdf = null; 
                }

                var res = _lessonService.InsertService(lesson);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadGateway, res);
                }
            }
            else
            {
                return Ok("Mã bài học đã có trong hệ thống.");
            }
        }
        [HttpPut]
        public IActionResult Update(Lesson lesson)
        {
            var res = _lessonService.UpdateService(lesson);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _lessonService.DeleteService(code);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
        [HttpPut]
        [Route("FilePdf")]
        public IActionResult UpdateLesson([FromForm] LessonPdf l)
        {
            var check = _lessonRepository.CheckDuplicateCode(l.LessonCode);
            if (check)
            {
                var lesson = new Lesson { LessonCode = l.LessonCode, LessonName = l.LessonName, LessonContent = l.LessonContent, CourseId = l.CourseId };
                if (l.File.Length > 0)
                {
                    using (var stream = l.File.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(l.File.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;

                        lesson.LessonPdf = fileUrl;
                    }
                }
                else
                {
                    lesson.LessonPdf = null;
                }

                var res = _lessonService.UpdateService(lesson);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadGateway, res);
                }
            }
            else
            {
                return Ok("Mã bài học chưa có trong hệ thống.");
            }
        }
    }
}
