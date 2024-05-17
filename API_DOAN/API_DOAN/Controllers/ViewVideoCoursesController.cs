using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/view_video_course")]
    [ApiController]
    public class ViewVideoCoursesController : ControllerBase
    {
        IVideoCourseRepository _videoCourseRepository; 
        public ViewVideoCoursesController(IVideoCourseRepository videoCourseRepository)
        {
            _videoCourseRepository = videoCourseRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _videoCourseRepository.GetAll();
            return Ok(res); 
        }
    }
}
