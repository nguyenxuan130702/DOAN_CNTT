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
    [Route("api/v1/assignment")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        IAssignmentRepository _assignmentRepository;
        IAssignmentService _assignmentService;
        private readonly Cloudinary _cloudinary;
        public AssignmentsController(IAssignmentRepository assignmentRepository, IAssignmentService assignmentService)
        {
            _assignmentRepository = assignmentRepository;
            _assignmentService = assignmentService;
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = _assignmentRepository.GetAll();
            return Ok(res);
        }
        [HttpGet]
        [Route("class")]
        public IActionResult Get(Guid classId)
        {
            var res = _assignmentRepository.GetByClass(classId); 
            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(Assignment t)
        {
            var res = _assignmentService.InsertService(t);
            return StatusCode(200, res);
        }
        [HttpPost]
        [Route("FilePdf")]
        public IActionResult PostwithPdf([FromForm] AssignmentFile l)
        {
            var check = _assignmentRepository.CheckDuplicateCode(l.AssignmentCode);
            if (!check)
            {
                var lesson = new Assignment { AssignmentId = Guid.NewGuid(), AssignmentCode = l.AssignmentCode, AssignmentName = l.AssignmentName, Deadline = l.Deadline, Description = l.Description, ClassId = l.ClassId };
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

                        lesson.AssignmentLink = fileUrl;
                    }
                }
                else
                {
                    lesson.AssignmentLink = null;
                }

                var res = _assignmentService.InsertService(lesson);
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
                return Ok("Mã tài liệu đã có trong hệ thống.");
            }
        }
        [HttpPut]
        public IActionResult Put(Assignment t)
        {
            var res = _assignmentService.UpdateService(t);
            return StatusCode(200, res);
        }
        [HttpPut]
        [Route("FilePdf")]
        public IActionResult PutWithFile([FromForm] AssignmentFile l)
        {
            var check = _assignmentRepository.CheckDuplicateCode(l.AssignmentCode);
            if (check)
            {
                var lesson = new Assignment { AssignmentId = l.AssignmentId, AssignmentCode = l.AssignmentCode, AssignmentName = l.AssignmentName, Deadline = l.Deadline, Description = l.Description, ClassId = l.ClassId };
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

                        lesson.AssignmentLink = fileUrl;
                    }
                }
                else
                {
                    lesson.AssignmentLink = null;
                }

                var res = _assignmentService.UpdateService(lesson);
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
                return Ok("Mã tài liệu không có trong hệ thống.");
            }
        }
        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var res = _assignmentService.DeleteService(code);
            return StatusCode(200, res);
        }
        [HttpDelete]
        [Route("any")]
        public IActionResult DeleteAny(string[] code)
        {
            var res = _assignmentService.DeleteAnyService(code);
            return Ok(res);
        }
    }
}
