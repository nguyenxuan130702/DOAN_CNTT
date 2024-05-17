using API_DOAN_Core.Entities;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class ResultService : BaseService<Result>, IResultService
    {
        IResultRepository _scoreRepository;
        public ResultService(IResultRepository scoreRepository) : base(scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }
        protected override void ValidateObject(Result entity)
        {
            var isDuplicate = _scoreRepository.CheckDuplicateCode(entity.ResultCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_QuestionCode);
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _scoreRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_QuestionCode_No);
            }
        }
    }
}
