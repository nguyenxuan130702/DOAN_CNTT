using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/view_teacher_course")]
    [ApiController]
    public class ViewTeacherCoursesController : ControllerBase
    {
        IViewTeacherCourseRepository _courseRepository;
        public ViewTeacherCoursesController(IViewTeacherCourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _courseRepository.GetAll(); 
            return Ok(res);
        }
    }
}
