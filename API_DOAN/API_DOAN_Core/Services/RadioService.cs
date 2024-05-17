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
    public class RadioService : BaseService<Radio>, IRadioService
    {
        IRadioRepository _radioRepository; 
        public RadioService(IRadioRepository radioRepository) : base(radioRepository) 
        {
            _radioRepository = radioRepository;
        }
        protected override void ValidateObject(Radio entity)
        {
            var isDuplicate = _radioRepository.CheckDuplicateCode(entity.RadioCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_RadioCode);
            }
        }

        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _radioRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_RadioCode_No);
            }
        }
    }
}
