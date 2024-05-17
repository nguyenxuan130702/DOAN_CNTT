using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using API_DOAN_Core.Entities;

namespace API_DOAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryController()
        {
            var account = new Account(
                "da5nbr4gk",
                "214791311982313",
                "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk"); 
            _cloudinary = new Cloudinary(account);
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file"); 
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
                var uploadResult = _cloudinary.Upload(uploadParams);

                string imageUrl = uploadResult.Uri.ToString();

                return Ok(new { imageUrl }); 
            }
        }
        [HttpPost]
        [Route("uploadpdf")]
        public IActionResult UploadFilePdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Không có file được chọn"); 
            }
            else
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = "unique_id_for_uploaded_pdf"
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);

                    var fileUrl = uploadResult.Uri.AbsoluteUri; 
                    return Ok(fileUrl); 
                }
            }
        }
        [HttpPost]
        [Route("uploadVideo")]
        
        public IActionResult UploadVideo(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded"); 
            }
            else
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = "desited_public_id",
                        Overwrite = true
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);

                    var fileUrl = uploadResult.Uri.AbsoluteUri;
                    return Ok(fileUrl);
                }
            }

            //var uploadParams = new VideoUploadParams
            //{
            //    File = new FileDescription(file.FileName, file.OpenReadStream()),
            //    PublicId = "desited_public_id",
            //    Overwrite = true
            //}; 

            //var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            //if(uploadResult.Error != null)
            //{
            //    return BadRequest(uploadResult.Error.Message); 
            //}

            //return Ok(uploadResult);
        }
    }
}