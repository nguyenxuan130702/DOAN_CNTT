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
    [Route("api/v1/submit")]
    [ApiController]
    public class SubmitsController : ControllerBase
    {
        ISubmitRepository _submitRepository;
        ISubmitService _submitService;
        private readonly Cloudinary _cloudinary;
        public SubmitsController(ISubmitRepository submitRepository, ISubmitService submitService)
        {
            _submitRepository = submitRepository;
            _submitService = submitService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _submitRepository.GetAll();
            return StatusCode(200, res); 
        }
        [HttpGet]
        [Route("submit_user_assignment")]
        public IActionResult Get(Guid userId, Guid assignmentId)
        {
            var res = _submitRepository.GetByUserAndAssignment(userId, assignmentId);
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("submit_user")]
        public IActionResult Get(Guid userId)
        {
            var res = _submitRepository.GetByUser(userId); 
            return StatusCode(200, res);
        }
        [HttpGet]
        [Route("submit_assignment")]
        public IActionResult Get1(Guid assignmentId)
        {
            var res = _submitRepository.GetByAss(assignmentId);
            return StatusCode(200, res);
        }
        [HttpPost]
        public IActionResult Post(Submit s)
        {
            var res = _submitService.InsertService(s); 
            return StatusCode(200, res);
        }
        [HttpPost]
        [Route("FilePdf")]
        public IActionResult PostwithPdf([FromForm] SubmitForm l)
        {
            var check = _submitRepository.CheckDuplicateCode(l.SubmitCode);
            if (!check)
            {
                var submit = new Submit { SubmitId = Guid.NewGuid(), SubmitCode = l.SubmitCode, AssignmentId = l.AssignmentId, UserId = l.UserId, Date = l.Date };
                if (l.File.Length > 0)
                {
                    using (var stream = l.File.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(l.File.FileName, stream),
                            PublicId = "unique_id_for_uploaded_pdf"
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var fileUrl = uploadResult.Uri.AbsoluteUri;

                        submit.SubmitLink = fileUrl;
                    }
                }
                else
                {
                    submit.SubmitLink = null;
                }

                var res = _submitService.InsertService(submit);
                if (res.Success == true)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadGateway, res);
                }
            }
            else
            {
                return Ok("Mã bài làm đã có trong hệ thống.");
            }
        }
        [HttpPut]
        public IActionResult Put(Submit s)
        {
            var res = _submitService.UpdateService(s); 
            return StatusCode(200, res);
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _submitService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _submitService.DeleteAnyService(code);
            return Ok(res); 
        }
    }
}

