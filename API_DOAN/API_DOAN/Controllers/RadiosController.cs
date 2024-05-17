using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/radio")]
    [ApiController]
    public class RadiosController : ControllerBase
    {
        IRadioRepository _radioRepository;
        IRadioService _radioService;
        public RadiosController(IRadioRepository radioRepository, IRadioService radioService)
        {
            _radioRepository = radioRepository;
            _radioService = radioService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = _radioRepository.GetAll();
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var radios = _radioRepository.Get(code);
            return StatusCode(200, radios);
        }
        [HttpPost]
        public IActionResult Insert(Radio radio)
        {
            var res = _radioService.InsertService(radio);
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
        public IActionResult Update(Radio radio)
        {
            var res = _radioService.UpdateService(radio);
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
            var res = _radioService.DeleteService(code);
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
