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
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository, IDisposable
    {
        public TeacherRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateValidate(string code)
        {
            var sqlCheck = "SELECT TeacherCode FROM Teacher c WHERE c.TeacherCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public Teacher GetTeacherByEmail(string userEmail)
        {
            var sqlCheck = "SELECT * FROM Teacher c WHERE c.TeacherEmail = @email";
            var parameters = new DynamicParameters();
            parameters.Add("@email", userEmail);

            var res = _dbContext.Connection.QueryFirstOrDefault<Teacher>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
