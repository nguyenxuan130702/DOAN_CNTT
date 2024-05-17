using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/viewcourdaiso")]
    [ApiController]
    public class CourseDaiSoController : ControllerBase
    {
        ICourseDaiSoRepository _courseDaiSoRepository;
        ICourseDaiSoService _courseDaiSoService;
        public CourseDaiSoController(ICourseDaiSoRepository courseRepository, ICourseDaiSoService courseService)
        {
            _courseDaiSoRepository = courseRepository;
            _courseDaiSoService = courseService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _courseDaiSoRepository.GetAll();
            return StatusCode(200, res);
        }
    }
}
