using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/enrollment")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        IEnrollmentRepository _enrollmentRepository;
        IEnrollmentService _enrollmentService; 
        IViewUserEnrollmentRepository _viewUserEnrollmentRepository;
        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IEnrollmentService enrollmentService, IViewUserEnrollmentRepository viewUserEnrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _enrollmentService = enrollmentService;
            _viewUserEnrollmentRepository = viewUserEnrollmentRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _enrollmentRepository.GetAll(); 
            return Ok(res);
        }
        [HttpGet]
        [Route("user_enrollment")]
        public IActionResult GetUser()
        {
            var res = _viewUserEnrollmentRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("user_enrollment_teacher_course")]
        public IActionResult GetUserEnrollment_TeacherCourse(Guid teacher_CourseId)
        {
            var res = _viewUserEnrollmentRepository.GetUserEnrollmentByTeacher_Course(teacher_CourseId);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("user_teacher_course")]
        public IActionResult Get(Guid userId, Guid teacher_course)
        {
            var res = _enrollmentRepository.GetByUserIdAndTeacher_CourseId(userId, teacher_course);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(Enrollment t)
        {
            var res = _enrollmentService.InsertService(t);
            return StatusCode(200, res);
        }
        [HttpPut]
        public IActionResult Put(Enrollment t)
        {
            var res = _enrollmentService.UpdateService(t);
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _enrollmentService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _enrollmentService.DeleteAnyService(code);
            return Ok(res);
        }
    }
}
