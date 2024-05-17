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
    public class UserService : BaseService<User>, IUserService
    {
        IUserRepository _userRepository; 
        public UserService(IUserRepository userRepository) : base(userRepository) 
        {
            _userRepository = userRepository;
        }
        protected override void ValidateObject(User entity) 
        {
            var isDuplicate = _userRepository.CheckDuplicateValidate(entity.UserCode);
            if (isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_UserCode);
            }
        }

        protected override void ValidateUpdateDelete(string code)
        {
            var isDuplicate = _userRepository.CheckDuplicateValidate(code);
            if (!isDuplicate)
            {
                throw new DOANValidateException(API_DOAN_Core.Resource.Resource1.Exception_Error_UserCode_No);
            }
        }
    }
}
