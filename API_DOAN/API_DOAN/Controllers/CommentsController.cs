using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Repository;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_DOAN.Controllers
{
    [Route("api/v1/comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        ICommentRepository _commentRepository;
        ICommentService _commentService;
        private readonly Cloudinary _cloudinary;
        public CommentsController(ICommentRepository commentRepository, ICommentService commentService)
        {
            _commentRepository = commentRepository;
            _commentService = commentService;
            var account = new Account(
               "da5nbr4gk",
               "214791311982313",
               "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _commentRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("code")]
        public IActionResult GetCode(string code)
        {
            var res = _commentRepository.Get(code);
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("threadsId")]
        public IActionResult GetThread(Guid threadsId)
        {
            var res = _commentRepository.GetThreadsId(threadsId);
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("user")]
        public IActionResult GetUser(Guid userId)
        {
            var res = _commentRepository.GetUser(userId); 
            return StatusCode(200, res);
        }
        [HttpPost]
        public IActionResult Insert(Comment course)
        {
            var res = _commentService.InsertService(course);
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
        public IActionResult Update(Comment course)
        {
            var res = _commentService.UpdateService(course);
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
            var res = _commentService.DeleteService(code);
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
        [Route("DeleteAny")]
        public IActionResult DeleteAny(string[] codes)
        {
            var res = _commentService.DeleteAnyService(codes);
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
        public IActionResult PostNew([FromForm] CommentImage p)
        {
            var check = _commentRepository.CheckDuplicateCode(p.CommentCode);
            if (check)
            {
                return Ok("Mã comment đã tồn tại.");
            }
            else
            {

                var comment = new Comment { CommentId = Guid.NewGuid(), CommentCode = p.CommentCode, CommentContent = p.CommentContent, UserId = p.UserId, ThreadsId = p.ThreadsId, Created_at = p.Created_at};
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

                        comment.CommentImage = imageUrl;
                    }
                }
                else
                {
                    comment.CommentImage = "";
                }

                var res = _commentService.InsertService(comment);
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

    }
}
