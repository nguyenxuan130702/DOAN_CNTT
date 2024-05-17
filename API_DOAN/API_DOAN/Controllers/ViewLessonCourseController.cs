using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/viewlessoncourse")]
    [ApiController]
    public class ViewLessonCourseController : ControllerBase
    {
        ILessonCourseRepository _lessonCourseRepository;
        ILessonCourseService _lessonCourseService; 
        public ViewLessonCourseController(ILessonCourseRepository lessonCourseRepository, ILessonCourseService lessonCourseService)
        {
            _lessonCourseRepository = lessonCourseRepository;
            _lessonCourseService = lessonCourseService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _lessonCourseRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("page")]
        public IActionResult Page(int page, int pageSize)
        {
            var res = _lessonCourseRepository.GetPage(page, pageSize); 
            return StatusCode(200, res);
        }
    }
}
