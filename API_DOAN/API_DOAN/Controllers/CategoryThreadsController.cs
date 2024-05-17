using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/viewthread")]
    [ApiController]
    public class CategoryThreadsController : ControllerBase
    {
        ICategoryThreadRepository _categoryThreadRepository;
        ICategoryThreadService _categoryThreadService; 
        public CategoryThreadsController(ICategoryThreadRepository categoryThreadRepository, ICategoryThreadService categoryThreadService)
        {
            _categoryThreadRepository = categoryThreadRepository;
            _categoryThreadService = categoryThreadService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _categoryThreadRepository.GetAll();
            return StatusCode(200, res); 
        }
    }
}
