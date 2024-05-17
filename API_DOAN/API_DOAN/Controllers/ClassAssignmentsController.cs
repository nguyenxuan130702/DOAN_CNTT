using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/view_class_assignment")]
    [ApiController]
    public class ClassAssignmentsController : ControllerBase
    {
        IViewClassAssignmentRepository _classRepository; 
        public ClassAssignmentsController(IViewClassAssignmentRepository classRepository)
        {
            _classRepository = classRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _classRepository.GetAll(); 
            return Ok(res);
        }
        [HttpGet]
        [Route("teacher_course")]
        public IActionResult Get(Guid teacher_CourseId)
        {
            var res = _classRepository.GetByTeacher_Course(teacher_CourseId);
            return Ok(res);
        }
    }
}
