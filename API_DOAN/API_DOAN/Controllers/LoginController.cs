using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserRepository _userRepository;
        ITeacherRepository _teacherRepository; 

        public LoginController(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }
        [HttpPost]
        [Route("teacher")]
        public IActionResult LoginTeacher([FromBody] LoginModel model)
        {
            var teacher = _teacherRepository.GetTeacherByEmail(model.email); 
            if(teacher != null && teacher.TeacherPassword == model.password)
            {
                return StatusCode(200, teacher); 
            }
            else
            {
                return BadRequest(new { message = "Đăng nhập không thành công." });
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userRepository.GetUserByEmail(model.email);
            if (user != null && user.UserPassword == model.password)
            {
                return StatusCode(200, user); 
            }
            else
            {
                return BadRequest(new { message = "Đăng nhập không thành công." }); 
            }
        }
    }
}
