using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository, IDisposable
    {
        public UserRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateValidate(string code)
        {
            var sqlCheck = "SELECT UserCode FROM User c WHERE c.UserCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public User GetUserByEmail(string userEmail)
        {
            var sqlCheck = "SELECT * FROM User c WHERE c.UserEmail = @email"; 
            var parameters = new DynamicParameters();
            parameters.Add("@email", userEmail);

            var res = _dbContext.Connection.QueryFirstOrDefault<User>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
