using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/teacher")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        ITeacherRepository _teacherRepository;
        ITeacherService _teacherService; 
        public TeachersController(ITeacherRepository teacherRepository, ITeacherService teacherService)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _teacherRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpPost]
        public IActionResult Post(Teacher t)
        {
            var res = _teacherService.InsertService(t); 
            return StatusCode(200, res);
        }
        [HttpPut]
        public IActionResult Put(Teacher t)
        {
            var res = _teacherService.UpdateService(t); 
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _teacherService.DeleteService(code); 
            return StatusCode(200, res);
        }
    }
}
