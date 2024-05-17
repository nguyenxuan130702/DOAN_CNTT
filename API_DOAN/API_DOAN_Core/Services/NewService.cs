using API_DOAN_Core.Entities;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class NewService : BaseService<New>, INewService
    {
        INewRepository _newRepository;
        IUnitOfWork _unitOfWork; 

        public NewService(INewRepository newRepository, IUnitOfWork unitOfWork) : base(newRepository)
        {
            _newRepository = newRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<New> PostNewImage(NewImage p)
        {
            throw new NotImplementedException();

        }

        protected override void ValidateObject(New entity)
        {
            //thực hiện kiểm tra mã nhân viên 
            var isDuplicate = _newRepository.CheckDuplicateCode(entity.NewCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_NewCode_Yes); 
            }
        }
        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _newRepository.CheckDuplicateCode(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_NewCode_No); 
            }
        }
        protected override void ProcessAfterSave(New entity)
        {
            //base.ProcessAfterSave(entity);
        }
    }
}
