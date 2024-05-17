using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/video_question")]
    [ApiController]
    public class Video_QuestionController : ControllerBase
    {
        IVideo_QuestionRepository _video_questionRepository;
        IVideo_QuestionService _video_questionService; 
        public Video_QuestionController(IVideo_QuestionRepository video_questionRepository, IVideo_QuestionService video_questionService)
        {
            _video_questionRepository = video_questionRepository;
            _video_questionService = video_questionService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _video_questionRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("video")]
        public IActionResult GetVideo(Guid videoId)
        {
            var res = _video_questionRepository.GetQuestionVideo(videoId);
            return StatusCode(200, res); 
        }
        [HttpPost]
        public IActionResult Post(Video_Question v)
        {
            var res = _video_questionService.InsertService(v); 
            return StatusCode(200, res);
        }
        [HttpPost]
        [Route("import")]
        public IActionResult Import(IFormFile excelFile)
        {
            var res = _video_questionService.ImportEmployee(excelFile);
            return StatusCode(200, res);
        }
        [HttpPut]
        public IActionResult Put(Video_Question v)
        {
            var res = _video_questionService.UpdateService(v); 
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _video_questionService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _video_questionService.DeleteAnyService(code); 
            return StatusCode(200, res);
        }
    }
}
