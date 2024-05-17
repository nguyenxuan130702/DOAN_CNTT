using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/view_video_question")]
    [ApiController]
    public class ViewVideoQuestionController : ControllerBase
    {
        IVideoQuestionRepository _videoQuestionRepository;
        IVideoQuestionService _videoQuestionService; 
        public ViewVideoQuestionController(IVideoQuestionRepository videoQuestionRepository, IVideoQuestionService videoQuestionService)
        {
            _videoQuestionRepository = videoQuestionRepository;
            _videoQuestionService = videoQuestionService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _videoQuestionRepository.GetAll();
            return StatusCode(200, res); 
        }

    }
}
