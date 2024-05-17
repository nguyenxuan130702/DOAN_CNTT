using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/learning")]
    [ApiController]
    public class LearningsController : ControllerBase
    {
        ILearningRepository _learningRepository;
        ILearningService _learningService; 
        public LearningsController(ILearningRepository learningRepository, ILearningService learningService)
        {
            _learningRepository = learningRepository;
            _learningService = learningService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _learningRepository.GetAll();
            return Ok(res); 
        }
        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(Guid userId)
        {
            var res = _learningRepository.GetLearning(userId); 
            return Ok(res);
        }
        [HttpGet]
        [Route("video")]
        public IActionResult GetLearningVideo(Guid videoId)
        {
            var res = _learningRepository.GetLearningVideo(videoId);
            return Ok(res); 
        }
        [HttpPost]
        public IActionResult Post(Learning l)
        {
            var res = _learningService.InsertService(l);
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Put(Learning l)
        {
            var res = _learningService.UpdateService(l);
            return Ok(res); 
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _learningService.DeleteService(code);
            return Ok(res); 
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] codes)
        {
            var res = _learningService.DeleteAnyService(codes);
            return Ok(res); 
        }
    }
}
