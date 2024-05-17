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
    [Route("api/v1/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        IClassRepository _classResitory;
        IClassService _classService;
        private readonly Cloudinary _cloudinary;
        public ClassController(IClassRepository classResitory, IClassService classService)
        {
            _classResitory = classResitory;
            _classService = classService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _classResitory.GetAll(); 
            return Ok(res);
        }
        [HttpGet]
        [Route("teacher_course")]
        public IActionResult Get(Guid teacher_CourseId)
        {
            var res = _classResitory.GetByTeacher_Course(teacher_CourseId); 
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(Class t)
        {
            var res = _classService.InsertService(t);
            return StatusCode(200, res);
        }
        [HttpPost]
        [Route("FilePdf")]
        public IActionResult PostwithPdf([FromForm] ClassFile l)
        {
            var check = _classResitory.CheckDuplicateCode(l.ClassCode);
            if (!check)
            {
                var lesson = new Class { ClassId = Guid.NewGuid(), ClassCode = l.ClassCode, ClassName = l.ClassName, ClassLink = l.ClassLink, Date = l.Date, Time = l.Time, Teacher_CourseId = l.Teacher_CourseId };
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

                        lesson.DocumentLink = fileUrl;
                    }
                }
                else
                {
                    lesson.DocumentLink = null;
                }

                var res = _classService.InsertService(lesson);
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
                return Ok("Mã buổi học đã có trong hệ thống.");
            }
        }
        [HttpPut]
        public IActionResult Put(Class t)
        {
            var res = _classService.UpdateService(t);
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _classService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _classService.DeleteAnyService(code);
            return Ok(res);
        }
    }
}
