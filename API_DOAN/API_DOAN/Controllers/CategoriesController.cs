using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryRepository _categoryRepository;
        ICategoryService _categoryService; 
        public CategoriesController(ICategoryRepository categoryRepository, ICategoryService categoryService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _categoryRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var res = _categoryRepository.Get(code); 
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("page")]
        public IActionResult GetPage(int page, int pageSize)
        {
            var res = _categoryRepository.GetPage(page, pageSize);
            return StatusCode(200, res);
        }
        [HttpPost]
        public IActionResult Insert(Category category)
        {
            var res = _categoryService.InsertService(category);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
        [HttpPut]
        public IActionResult Update(Category category)
        {
            var res = _categoryService.UpdateService(category);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _categoryService.DeleteService(code);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
    }
}
