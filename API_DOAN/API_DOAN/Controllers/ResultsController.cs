using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/result")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        IResultRepository _resultRepository;
        IResultService _resultService; 
        public ResultsController(IResultRepository resultRepository, IResultService resultService)
        {
            _resultRepository = resultRepository;
            _resultService = resultService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _resultRepository.GetAll(); 
            return Ok(res);
        }
        [HttpGet]
        [Route("user_assignment")]
        public IActionResult Get(Guid userId, Guid assignmentId)
        {
            var res = _resultRepository.GetByUser(userId, assignmentId);
            return Ok(res);
        }
        [HttpGet]
        [Route("assignment")]
        public IActionResult Get1(Guid assignmentId)
        {
            var res = _resultRepository.GetByAss(assignmentId);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(Result r)
        {
            var res = _resultService.InsertService(r); 
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Put(Result r)
        {
            var res = _resultService.UpdateService(r);
            return Ok(res); 
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _resultService.DeleteService(code);
            return Ok(res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _resultService.DeleteAnyService(code);
            return Ok(res);
        }

    }
}
