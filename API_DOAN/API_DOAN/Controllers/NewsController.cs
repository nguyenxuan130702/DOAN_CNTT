using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Repository;

namespace API_DOAN.Controllers
{
    [Route("api/v1/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        INewRepository _newRepository;
        INewService _newService;
        private readonly Cloudinary _cloudinary;
        public NewsController(INewRepository newRepository, INewService newService)
        {
            _newRepository = newRepository;
            _newService = newService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _newRepository.GetAll();
            return StatusCode(200, customers);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult Get(string code)
        {
            var customer = _newRepository.Get(code);
            return StatusCode(200, customer);
        }
        [HttpGet]
        [Route("page")]
        public IActionResult Page(int page, int pageSize)
        {
            var news = _newRepository.GetPage(page, pageSize);
            return StatusCode(200, news);
        }
        [HttpPost]
        public IActionResult Insert(New news)
        {
            var res = _newService.InsertService(news);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }

        [HttpPost]
        [Route("updateFile")]
        public async Task<IActionResult> PostwithImage([FromForm] NewImage p)
        {
            var check = _newRepository.CheckDuplicateCode(p.NewCode);
            if (check)
            {
                return Ok("Mã sản phẩm đã tồn tại");
            }
            else
            {
                var news = new New { NewId = p.NewId, NewCode = p.NewCode, NewContent = p.NewContent, NewDate = p.NewDate, CreatedBy = p.CreatedBy, CreatedDate = p.CreatedDate, NewTitle = p.NewTitle }; 
                if(p.Image.Length > 0)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Image", p.Image.FileName);
                    var path = Path.Combine(@"D:\DHCNHN\", @"ĐỒ ÁN\MAIN\", @"demodoan\src\assets\", p.Image.FileName); 
                    using (var stream = System.IO.File.Create(path))
                    {
                        await p.Image.CopyToAsync(stream); 
                    }
                    news.NewImage = "@/assets/News/" + p.Image.FileName; 
                }
                else
                {
                    news.NewImage = ""; 
                }
                var res = _newService.InsertService(news);
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
        public IActionResult Update(New news)
        {
            var res = _newService.UpdateService(news);
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
            var res = _newService.DeleteService(code);
            if (res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res);
            }
        }

        private void SetNewId(New entity)
        {
            var className = typeof(New).Name;
            var prop = typeof(New).GetProperty($"{className}Id");
            if (prop != null && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid)))
            {
                prop.SetValue(entity, Guid.NewGuid());
            }
        }

        [HttpPost]
        [Route("PostImage")]
        public IActionResult PostNew([FromForm] NewImage p)
        { 
            var check = _newRepository.CheckDuplicateCode(p.NewCode);
            if (check)
            {
                return Ok("Mã bản tin đã tồn tại.");
            }
            else
            {
                var news = new New { NewId = Guid.NewGuid(), NewCode = p.NewCode, NewContent = p.NewContent, NewDate = p.NewDate, CreatedBy = p.CreatedBy, CreatedDate = p.CreatedDate, NewTitle = p.NewTitle };
                if(p.Image.Length > 0)
                {
                    using (var stream = p.Image.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(p.Image.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        news.NewImage = imageUrl;
                    } 
                }
                else
                {
                    news.NewImage = ""; 
                }

                var res = _newService.InsertService(news);
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
        public IActionResult UpdateImage([FromForm] NewImage p)
        {
            var check = _newRepository.CheckDuplicateCode(p.NewCode);
            if (check)
            {
                var news = new New { NewId = p.NewId, NewCode = p.NewCode, NewTitle = p.NewTitle, NewContent = p.NewContent, CreatedBy = p.CreatedBy };
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

                        news.NewImage = imageUrl;
                    }
                }
                

                var res = _newService.UpdateService(news);
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
                return Ok("Mã bản tin không có trong hệ thống.");
            }
        }
    }
}
