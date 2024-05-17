using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseRepository _courseRepository;
        ICourseService _courseService;
        public CourseController(ICourseRepository courseRepository, ICourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = _courseRepository.GetAll();
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetByCode(string code)
        {
            var course = _courseRepository.Get(code);
            return StatusCode(200, course);
        }
        [HttpGet]
        [Route("page")]
        public IActionResult Page(int page, int pageSize)
        {
            var course = _courseRepository.GetPage(page, pageSize); 
            return StatusCode(200, course);
        }
        [HttpPost]
        public IActionResult Insert(Course course)
        {
            var res = _courseService.InsertService(course);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
        [HttpPut]
        public IActionResult Update(Course course)
        {
            var res = _courseService.UpdateService(course);
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
            var res = _courseService.DeleteService(code);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
    }
}
