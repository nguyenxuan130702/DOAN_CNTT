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
    [Route("api/v1/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepository _userRepository;
        IUserService _userService;
        private readonly Cloudinary _cloudinary;
        public UsersController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = _userRepository.GetAll();
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult Get(string code)
        {
            var users = _userRepository.Get(code); 
            return StatusCode(200, users);
        }
        [HttpGet]
        [Route("page")]
        public IActionResult Page(int page, int pageSize)
        {
            var users = _userRepository.GetPage(page, pageSize); 
            return StatusCode(200, users);
        }
        [HttpPost]
        public IActionResult Insert(User user)
        {
            
            var res = _userService.InsertService(user);
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
        public IActionResult Update(User user)
        {
            var res = _userService.UpdateService(user);
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
            var res = _userService.DeleteService(code);
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
        public IActionResult PostNew([FromForm] UserImage p)
        {
            var check = _userRepository.CheckDuplicateValidate(p.UserCode);
            if (check)
            {
                return Ok("Mã người dùng đã tồn tại.");
            }
            else
            {
                var user = new User { UserCode = p.UserCode, UserAccout = p.UserAccout, UserName = p.UserName, UserPassword = p.UserPassword, UserType = p.UserType, UserEmail = p.UserEmail };
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

                        user.UserImage = imageUrl;
                    }
                }
                else
                {
                    user.UserImage = null;
                }

                var res = _userService.InsertService(user);
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

        [HttpPut]
        [Route("UpdateImage")]
        public IActionResult UpdateImage([FromForm] UserImage p)
        {
            var check = _userRepository.CheckDuplicateValidate(p.UserCode);
            if (!check)
            {
                var user = new User {UserId = p.UserId, UserCode = p.UserCode, UserAccout = p.UserAccout, UserName = p.UserName, UserPassword = p.UserPassword, UserType = p.UserType, UserEmail = p.UserEmail };
                if (p.Image == null)
                {
                    user.UserImage = null;
                }
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

                        user.UserImage = imageUrl;
                    }
                }
                else
                {
                    user.UserImage = null;
                }

                var res = _userService.UpdateService(user);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, res);
                }
            }
            else
            {
                return Ok("Mã người dùng không có trong hệ thống."); 
            }
        }
    }
}
