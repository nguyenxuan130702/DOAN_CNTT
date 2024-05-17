using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/student_answer")]
    [ApiController]
    public class StudentAnswerController : ControllerBase
    {
        IStudentAnswerRepository _studentAnswerRepository;
        IStudentAnswerService _studentAnswerService; 
        public StudentAnswerController(IStudentAnswerRepository studentAnswerRepository, IStudentAnswerService studentAnswerService)
        {
            _studentAnswerRepository = studentAnswerRepository;
            _studentAnswerService = studentAnswerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _studentAnswerRepository.GetAll();
            return StatusCode(200, res); 
        }

        [HttpGet]
        [Route("byuser")]
        public IActionResult Get(Guid userId)
        {
            var res = _studentAnswerRepository.GetByUser(userId); 
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("byquestion")]
        public IActionResult GetQuestion(Guid questionId)
        {
            var res = _studentAnswerRepository.GetByQuestion(questionId); 
            return StatusCode(200, res);
        }
        [HttpPost]
        public IActionResult Insert(StudentAnswer sa)
        {
            var res = _studentAnswerService.InsertService(sa);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
        [HttpDelete]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _studentAnswerService.DeleteAnyService(code);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
    }
}
