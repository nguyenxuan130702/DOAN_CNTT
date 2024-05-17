using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Repository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/thread")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        IThreadsRepository _threadRepository;
        IThreadsService _threadService;
        private readonly Cloudinary _cloudinary;
        public ThreadsController(IThreadsRepository threadRepository, IThreadsService threadsService)
        {
            _threadRepository = threadRepository;
            _threadService = threadsService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _threadRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var res = _threadRepository.Get(code); 
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("categoryId")]
        public IActionResult GetCategory(Guid categoryId)
        {
            var res = _threadRepository.GetCategory(categoryId);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(Guid userId)
        {
            var res = _threadRepository.GetUser(userId);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("threadsId")]
        public IActionResult GetThread(Guid threadId)
        {
            var res = _threadRepository.GetThread(threadId);
            return StatusCode(200, res); 
        }
        [HttpPost]
        public IActionResult Insert(Threads course)
        {
            var res = _threadService.InsertService(course);
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
        public IActionResult Update(Threads course)
        {
            var res = _threadService.UpdateService(course);
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
            var res = _threadService.DeleteService(code);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }
        [HttpPost]
        [Route("PostImage")]
        public IActionResult PostNew([FromForm] ThreadsImage p)
        {
            var check = _threadRepository.CheckDuplicateCode(p.ThreadsCode);
            if (check)
            {
                return Ok("Mã bài đăng đã tồn tại.");
            }
            else
            {

                var thread = new Threads { ThreadsId = Guid.NewGuid(), ThreadsCode = p.ThreadsCode, ThreadsTitle = p.ThreadsTitle, ThreadsContent = p.ThreadsContent, CategoryId = p.CategoryId, UserId = p.UserId, Created_at = p.Created_at };
                if (p.Image.Length > 0)
                {
                    using (var stream = p.Image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(p.Image.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        thread.ThreadsImage = imageUrl;
                    }
                }
                else
                {
                    thread.ThreadsImage = "";
                }

                var res = _threadService.InsertService(thread);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _threadService.DeleteAnyService(code);
            return StatusCode(200, res); 
        }
    }

}
