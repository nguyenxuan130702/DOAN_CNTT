using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/score_view")]
    [ApiController]
    public class ScoreViewsController : ControllerBase
    {
        IScoreViewRepository _scoreviewRepository;
        IScoreViewService _scoreviewService; 
        public ScoreViewsController(IScoreViewRepository scoreviewRepository, IScoreViewService scoreviewService)
        {
            _scoreviewRepository = scoreviewRepository;
            _scoreviewService = scoreviewService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _scoreviewRepository.GetAll(); 
            return Ok(res);
        }
    }
}
