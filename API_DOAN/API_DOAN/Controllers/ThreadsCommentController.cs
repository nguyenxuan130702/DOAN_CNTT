using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/threadscomment")]
    [ApiController]
    public class ThreadsCommentController : ControllerBase
    {
        IThreadsCommentRepository _threadCommentRepository;
        IThreadsCommentService _threadCommentService; 
        public ThreadsCommentController(IThreadsCommentRepository threadCommentRepository, IThreadsCommentService threadCommentService)
        {
            _threadCommentRepository = threadCommentRepository;
            _threadCommentService = threadCommentService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _threadCommentRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("categoryId")]
        public IActionResult Get(Guid categoryId)
        {
            var res = _threadCommentRepository.GetCategory(categoryId);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("threadId")]
        public IActionResult GetThread(Guid threadId)
        {
            var res = _threadCommentRepository.GetThread(threadId);
            return StatusCode(200, res);
        }
    }
}
