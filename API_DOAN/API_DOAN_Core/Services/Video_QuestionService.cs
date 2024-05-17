using API_DOAN_Core.Entities;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class Video_QuestionService : BaseService<Video_Question>, IVideo_QuestionService
    {
        IVideo_QuestionRepository _videoQuestionRepository;
        IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public Video_QuestionService(IVideo_QuestionRepository videoRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(videoRepository)
        {
            _videoQuestionRepository = videoRepository;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;  
        }
        private void CheckFileImport(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length <= 0)
            {
                throw new DOANValidateException("File nhập khẩu không được để trống.");
            }
            if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new DOANValidateException("File nhập khẩu không đúng định dạng cho phép.");
            }
        }

        public IEnumerable<Video_Question> ImportEmployee(IFormFile excelFile)
        {
            CheckFileImport(excelFile);
            var questions = new List<Video_QuestionImport>();

            using(var stream = new MemoryStream())
            {
                //Copy tệp vào stream 
                excelFile.CopyTo(stream);
                //Thực hiện đọc dữ liệu 
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    if(worksheet != null)
                    {
                        var rowCount = worksheet.Dimension.Rows;
                        _unitOfWork.BeginTransaction();
                        var i = 1;
                        for(int row = 2; row <= rowCount; row++)
                        {
                            string str = worksheet.Cells[row, 10]?.Value?.ToString()?.Trim();
                            Guid guid;
                            Guid done;
                            if (Guid.TryParse(str, out guid))
                            {
                                done = guid;
                            }
                            else
                            {
                                done = Guid.NewGuid();
                            }

                            var questionImport = new Video_QuestionImport
                            {
                                Video_QuestionCode = worksheet.Cells[row, 1].Value == null ? string.Empty : worksheet.Cells[row, 1].Value.ToString(),
                                Video_QuestionContent = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Video_QuestionImage = worksheet.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                Video_QuestionSuggest = worksheet.Cells[row, 4]?.Value?.ToString()?.Trim(),
                                Video_QuestionResult = worksheet.Cells[row, 5]?.Value?.ToString()?.Trim(),
                                Video_QuestionAnswerA = worksheet.Cells[row, 6]?.Value?.ToString()?.Trim(),
                                Video_QuestionAnswerB = worksheet.Cells[row, 7]?.Value?.ToString()?.Trim(),
                                Video_QuestionAnswerC = worksheet.Cells[row, 8]?.Value?.ToString()?.Trim(),
                                Video_QuestionAnswerD = worksheet.Cells[row, 9]?.Value?.ToString()?.Trim(),
                                VideoId = done
                            };
                            //validate dữ liệu xem mã đã trùng hay chưa
                            var isDuplicate = _unitOfWork.Video_Question.CheckDuplicateCode(questionImport.Video_QuestionCode);
                            if (isDuplicate)
                            {
                                questionImport.ImportValidateError.Add($"Mã câu hỏi {questionImport.Video_QuestionCode} đã tồn tại");
                            }
                            if (questionImport.ImportValidateError?.Count == 0)
                            {
                                var question = mapper.Map<Video_Question>(questionImport);
                                var res = _unitOfWork.Video_Question.Insert(question);
                                if (res > 0)
                                {
                                    questionImport.IsImported = true;
                                }
                            }
                            questions.Add(questionImport);
                        }
                        _unitOfWork.Commit(); 
                    }
                }
                return questions;
            }
        }

        protected override void ValidateObject(Video_Question entity)
        {
            // Thuc hien kiem tra ma nhan vien 
            var isDuplicate = _videoQuestionRepository.CheckDuplicateCode(entity.Video_QuestionCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoObject);
            }
        }

        protected override void ValidateUpdateDelete(string code)
        {
            //thực hiện kiểm tra mã 
            var isDuplicate = _videoQuestionRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_VideoCode);
            }
        }
    }
}
