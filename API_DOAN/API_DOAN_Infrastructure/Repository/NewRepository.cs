using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class NewRepository : BaseRepository<New>, INewRepository, IDisposable
    {
        public NewRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT NewCode FROM New c WHERE c.NewCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }
        public void Dispose()
        {
            _dbContext.Connection.Dispose(); 
        }
    }
}
