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
    [Route("api/v1/question")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        IQuestionRepository _questionRepository;
        IQuestionService _questionService;
        private readonly Cloudinary _cloudinary;
        public QuestionsController(IQuestionRepository questionRepository, IQuestionService questionService)
        {
            _questionRepository = questionRepository;
            _questionService = questionService;
            var account = new Account(
               "da5nbr4gk",
               "214791311982313",
               "1PD3uMD5hO6rS55Ja3HX0Fa3Uxk");
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var videos = _questionRepository.GetAll();
            return StatusCode(200, videos);
        }
        [HttpGet]
        [Route("code")]
        public IActionResult Get(string code)
        {
            var question = _questionRepository.Get(code);
            return StatusCode(200, question);
        }
        [HttpGet]
        [Route("lessonId")]
        public IActionResult GetBylesson(Guid id)
        {
            var question = _questionRepository.GetByLesson(id);
            return StatusCode(200, question); 
        }
        [HttpPost]
        public IActionResult Insert(Question question)
        {
            var res = _questionService.InsertService(question);
            if (res.Success == true)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadGateway, res);
            }
        }
        [HttpPost]
        [Route("uploadImage")]
        public async Task<IActionResult> uploadFile([FromForm] QuestionImage q)
        {
            var check = _questionRepository.CheckDuplicateCode(q.QuestionCode);
            if (check)
            {
                return Ok("Mã câu hỏi đã tồn tại");
            }
            else
            {
                var questions = new Question { QuestionId = q.QuestionId, QuestionCode = q.QuestionCode, CreatedBy = q.CreatedBy, CreatedDate = q.CreatedDate, QuestionResult = q.QuestionResult, Checked = q.Checked }; 
                //Kiểm tra ảnh câu hỏi
                if (q.ImageQuestion.Length > 0)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Image", p.Image.FileName);
                    var path = Path.Combine(@"D:\DHCNHN\", @"ĐỒ ÁN\MAIN\", @"demodoan\src\assets\Question\", q.ImageQuestion.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await q.ImageQuestion.CopyToAsync(stream);
                    }
                    questions.QuestionImage = "@/assets/Question/" + q.ImageQuestion; 
                }
                else
                {
                    questions.QuestionImage = ""; 
                }
                //Kiểm tra ảnh gợi ý trả lời
                if (q.QuestionSuggest.Length > 0)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Image", p.Image.FileName);
                    var path = Path.Combine(@"D:\DHCNHN\", @"ĐỒ ÁN\MAIN\", @"demodoan\src\assets\Question\", q.ImageQuestion.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await q.QuestionSuggest.CopyToAsync(stream);
                    }
                    questions.QuestionSuggest = "@/assets/Question/" + q.QuestionSuggest;
                }
                else
                {
                    questions.QuestionSuggest = "";
                }
                //Kiểm tra ảnh câu trả lời 
                if (q.ResultImage.Length > 0)
                {
                    //var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Image", p.Image.FileName);
                    var path = Path.Combine(@"D:\DHCNHN\", @"ĐỒ ÁN\MAIN\", @"demodoan\src\assets\Question\", q.ImageQuestion.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await q.ImageQuestion.CopyToAsync(stream);
                    }
                    questions.ResultImage = "@/assets/Question/" + q.ResultImage;
                }
                else
                {
                    questions.ResultImage = "";
                }


                var res = _questionService.InsertService(questions);
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
        public IActionResult Update(Question question)
        {
            var res = _questionService.UpdateService(question);
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
            var res = _questionService.DeleteService(code);
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
            var res = _questionService.DeleteAnyService(codes); 
            if(res.Success == true)
            {
                return StatusCode(200, res);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, res); 
            }
        }

        [HttpPost]
        [Route("import")]
        public IActionResult Import(IFormFile excelFile)
        {
            var res = _questionService.ImportEmployee(excelFile);
            return Ok(res); 
        }

        [HttpPost]
        [Route("PostImage")]
        public IActionResult Post([FromForm] QuestionImage q) 
        {
            var check = _questionRepository.CheckDuplicateCode(q.QuestionCode);
            if (check)
            {
                return Ok("Mã câu hỏi đã tồn tại");
            }
            else
            {
                var questions = new Question {QuestionId = Guid.NewGuid(), QuestionCode = q.QuestionCode, CreatedBy = q.CreatedBy, CreatedDate = q.CreatedDate, QuestionResult = q.QuestionResult, Checked = q.Checked, AnswerA = q.AnswerA, AnswerB = q.AnswerB, AnswerC = q.AnswerC, AnswerD = q.AnswerD, QuestionContent = q.QuestionContent, LessonId = q.LessonId };
                if (q.ImageQuestion == null || q.ImageQuestion.Length == 0)
                {
                    questions.QuestionImage = ""; 
                }
                else
                {
                    using (var stream = q.ImageQuestion.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.ImageQuestion.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.QuestionImage = imageUrl;
                    }
                }

                //upload file ảnh cho anh goi y tra loi 
                if(q.QuestionSuggest == null || q.QuestionSuggest.Length == 0)
                {
                    questions.QuestionSuggest = null; 
                }
                else
                {
                    using (var stream = q.QuestionSuggest.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.QuestionSuggest.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.QuestionSuggest = imageUrl;
                    }
                }

                //upload file ảnh cho ảnh kết quả 
                if(q.ResultImage == null || q.ResultImage.Length == 0)
                {
                    questions.ResultImage = "";
                }
                else
                {
                    using (var stream = q.ResultImage.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.ResultImage.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.ResultImage = imageUrl;
                    }
                }

                var res = _questionService.InsertService(questions);
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
        public IActionResult Update([FromForm] QuestionImage q)
        {
            var check = _questionRepository.CheckDuplicateCode(q.QuestionCode);
            if (!check)
            {
                return Ok("Mã câu hỏi chưa tồn tại");
            }
            else
            {
                var questions = new Question { QuestionId = q.QuestionId, QuestionCode = q.QuestionCode, CreatedBy = q.CreatedBy, CreatedDate = q.CreatedDate, QuestionResult = q.QuestionResult, Checked = q.Checked, AnswerA = q.AnswerA, AnswerB = q.AnswerB, AnswerC = q.AnswerC, AnswerD = q.AnswerD, QuestionContent = q.QuestionContent, LessonId = q.LessonId };
                if (q.ImageQuestion == null || q.ImageQuestion.Length == 0)
                {
                    questions.QuestionImage = "";
                }
                else
                {
                    using (var stream = q.ImageQuestion.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.ImageQuestion.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.QuestionImage = imageUrl;
                    }
                }

                //upload file ảnh cho anh goi y tra loi 
                if (q.QuestionSuggest == null || q.QuestionSuggest.Length == 0)
                {
                    questions.QuestionSuggest = null;
                }
                else
                {
                    using (var stream = q.QuestionSuggest.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.QuestionSuggest.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.QuestionSuggest = imageUrl;
                    }
                }

                //upload file ảnh cho ảnh kết quả 
                if (q.ResultImage == null || q.ResultImage.Length == 0)
                {
                    questions.ResultImage = "";
                }
                else
                {
                    using (var stream = q.ResultImage.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(q.ResultImage.FileName, stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        string imageUrl = uploadResult.Uri.ToString();

                        questions.ResultImage = imageUrl;
                    }
                }

                var res = _questionService.InsertService(questions);
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
