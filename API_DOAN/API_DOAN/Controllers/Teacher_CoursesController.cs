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
    [Route("api/v1/teacher_course")]
    [ApiController]
    public class Teacher_CoursesController : ControllerBase
    {
        ITeacher_CourseRepository _courseRepository; 
        ITeacher_CourseService _courseService;
        private readonly Cloudinary _cloudinary;
        public Teacher_CoursesController(ITeacher_CourseRepository courseRepository, ITeacher_CourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _courseRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("teacher")]
        public IActionResult Get(Guid teacherId)
        {
            var res = _courseRepository.GetByTeacher(teacherId); 
            return StatusCode(200, res);
        }
        [HttpPost]
        public IActionResult Post(Teacher_Course t)
        {
            var res = _courseService.InsertService(t); 
            return StatusCode(200, res);
        }
        [HttpPut]
        public IActionResult Put(Teacher_Course t)
        {
            var res = _courseService.UpdateService(t); 
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _courseService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _courseService.DeleteAnyService(code);
            return Ok(res);
        }
        [HttpPost]
        [Route("PostImage")]
        public IActionResult PostNew([FromForm] Teacher_CourseFile p)
        {
            var check = _courseRepository.CheckDuplicateValidate(p.Teacher_CourseCode);
            if (check)
            {
                return Ok("Mã lớp học đã tồn tại.");
            }
            else
            {
                var user = new Teacher_Course { Teacher_CourseCode = p.Teacher_CourseCode, Teacher_CourseDescription = p.Teacher_CourseDescription, Teacher_CourseName = p.Teacher_CourseName, StartTime = p.StartTime, EndTime = p.EndTime, RegistrationStartTime = p.RegistrationStartTime, RegistrationEndTime = p.RegistrationEndTime, TeacherId = p.TeacherId };
                if (p.Image.Length > 0)
                {
                    using (var stream = p.Image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(p.Image.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        user.Teacher_CourseImage = imageUrl;
                    }
                }
                else
                {
                    user.Teacher_CourseImage = null;
                }

                var res = _courseService.InsertService(user);
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
