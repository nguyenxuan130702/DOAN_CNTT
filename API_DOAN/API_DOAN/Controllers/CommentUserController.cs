using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/commentuser")]
    [ApiController]
    public class CommentUserController : ControllerBase
    {
        ICommentUserRepository _commentUserRepository;
        ICommentUserService _commentUserService; 
        public CommentUserController(ICommentUserRepository commentUserRepository, ICommentUserService commentUserService)
        {
            _commentUserRepository = commentUserRepository;
            _commentUserService = commentUserService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _commentUserRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("threadId")]
        public IActionResult Get(Guid threadId)
        {
            var res = _commentUserRepository.GetThreads(threadId); 
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("page")]
        public IActionResult GetPage(int page, int pageSize)
        {
            var res = _commentUserRepository.GetPage(page, pageSize);
            return StatusCode(200, res);    
        }
    }
}
