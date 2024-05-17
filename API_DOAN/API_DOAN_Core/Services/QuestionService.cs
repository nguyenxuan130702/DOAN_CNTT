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
    public class QuestionService : BaseService<Question>, IQuestionService
    {
        IQuestionRepository _questionRepository;
        IUnitOfWork _unitOfWork;
        private readonly IMapper mapper; 
        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(questionRepository) 
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        private void CheckFileImport(IFormFile excelFile)
        {
            if(excelFile == null || excelFile.Length <= 0)
            {
                throw new DOANValidateException("File nhập khẩu không được để trống."); 
            }
            if(!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new DOANValidateException("File nhập khẩu không đúng định dạng cho phép."); 
            }
        }

        public IEnumerable<Question> ImportEmployee(IFormFile excelFile)
        {
            CheckFileImport(excelFile);
            var questions = new List<QuestionImport>();
            using (var stream = new MemoryStream())
            {
                //Copy tệp vào stream 
                excelFile.CopyTo(stream);
                //Thực hiện đọc dữ liệu 
                using (var package = new ExcelPackage(stream))
                {
                    //Sheet đọc dữ liệu 
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; 
                    if(worksheet != null)
                    {
                        //Tổng số dòng dữ liệu 
                        var rowCount = worksheet.Dimension.Rows;
                        _unitOfWork.BeginTransaction();
                        var i = 1; 
                        for(int row = 2; row <= rowCount; row++)
                        {
                            string str = worksheet.Cells[row, 10]?.Value?.ToString()?.Trim();
                            Guid guid;
                            Guid done; 
                            if(Guid.TryParse(str, out guid))
                            {
                                done = guid; 
                            }
                            else
                            {
                                done = Guid.NewGuid();
                            }

                            var questionImport = new QuestionImport
                            {
                                QuestionId = Guid.NewGuid(),
                                QuestionCode = worksheet.Cells[row, 1].Value == null ? string.Empty : worksheet.Cells[row, 1].Value.ToString(),
                                QuestionContent = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                QuestionImage = worksheet.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                QuestionSuggest = worksheet.Cells[row, 4]?.Value?.ToString()?.Trim(),
                                QuestionResult = worksheet.Cells[row, 5]?.Value?.ToString()?.Trim(),
                                AnswerA = worksheet.Cells[row, 6]?.Value?.ToString()?.Trim(),
                                AnswerB = worksheet.Cells[row, 7]?.Value?.ToString()?.Trim(),
                                AnswerC = worksheet.Cells[row, 8]?.Value?.ToString()?.Trim(),
                                AnswerD = worksheet.Cells[row, 9]?.Value?.ToString()?.Trim(),
                                LessonId = done
                            };
                            //validate dữ liệu xem mã đã trùng hay chưa 
                            var isDuplicate = _unitOfWork.Questions.CheckDuplicateCode(questionImport.QuestionCode);
                            if (isDuplicate)
                            {
                                questionImport.ImportValidateError.Add($"Mã câu hỏi {questionImport.QuestionCode} đã tồn tại"); 
                            }
                            if(questionImport.ImportValidateError?.Count == 0)
                            {
                                var question = mapper.Map<Question>(questionImport);
                                var res = _unitOfWork.Questions.Insert(question);
                                if(res > 0)
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

        protected override void ValidateObject(Question entity)
        {
            var isDuplicate = _questionRepository.CheckDuplicateCode(entity.QuestionCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_QuestionCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _questionRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_QuestionCode_No);
            }
        }
    }
}
