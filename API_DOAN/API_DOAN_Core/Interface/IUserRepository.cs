using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public bool CheckDuplicateValidate(string code);
        public User GetUserByEmail(string userEmail); 
    }
}
