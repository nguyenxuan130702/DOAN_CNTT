using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DOAN.Controllers
{
    [Route("api/v1/viewquestionlesson")]
    [ApiController]
    public class ViewQuestionLessonController : ControllerBase
    {
        IQuestionLessonRepository _courseLessonRepository;
        IQuestionLessonService _courseLessonService;

        public ViewQuestionLessonController(IQuestionLessonRepository lessonCourseRepository, IQuestionLessonService lessonCourseService)
        {
            _courseLessonRepository = lessonCourseRepository;
            _courseLessonService = lessonCourseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _courseLessonRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("page")]
        public IActionResult GetPage(int page, int pageSize)
        {
            var res = _courseLessonRepository.GetPage(page, pageSize);
            return StatusCode(200, res);
        }
    }
}
