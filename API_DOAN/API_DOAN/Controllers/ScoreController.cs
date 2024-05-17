using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/score")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        IScoreRepository _scoreRepository;
        IScoreService _scoreService; 
        public ScoreController(IScoreRepository scoreRepository, IScoreService scoreService)
        {
            _scoreRepository = scoreRepository;
            _scoreService = scoreService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _scoreRepository.GetAll();
            return Ok(res); 
        }
        [HttpGet]
        [Route("lesson_user")]
        public IActionResult GetScore(Guid userId, Guid lessonId)
        {
            var res = _scoreRepository.GetScores(userId, lessonId); 
            return Ok(res);
        }
        [HttpGet]
        [Route("user")]
        public IActionResult GetScoreUser(Guid userId)
        {
            var res = _scoreRepository.GetScoreUser(userId); 
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(Score s)
        {
            var res = _scoreService.InsertService(s); 
            return Ok(res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _scoreService.DeleteService(code);
            return Ok(res); 
        }
        [HttpPut]
        public IActionResult Put(Score s)
        {
            var res = _scoreService.UpdateService(s);
            return Ok(res); 
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _scoreService.DeleteAnyService(code);
            return Ok(res); 
        }
    }
}
