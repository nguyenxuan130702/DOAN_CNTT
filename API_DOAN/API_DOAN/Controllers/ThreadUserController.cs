using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/threaduser")]
    [ApiController]
    public class ThreadUserController : ControllerBase
    {
        IThreadUserRepository _threadUserRepository; 
        public ThreadUserController(IThreadUserRepository threadUserRepository)
        {
            _threadUserRepository = threadUserRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _threadUserRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("categoryId")]
        public IActionResult Get(Guid categoryId)
        {
            var res = _threadUserRepository.GetCategory(categoryId);
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("threadsId")]
        public IActionResult GetThread(Guid threadsId)
        {
            var res = _threadUserRepository.GetThread(threadsId);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("page")]
        public IActionResult Page(int page, int pageSize)
        {
            var res = _threadUserRepository.GetPage(page, pageSize);
            return StatusCode(200, res);
        }
    }
}
