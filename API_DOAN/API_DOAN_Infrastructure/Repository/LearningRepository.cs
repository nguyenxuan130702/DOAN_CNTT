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
    public class LearningRepository : BaseRepository<Learning>, ILearningRepository, IDisposable
    {
        public LearningRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT LearningCode FROM Learning c WHERE c.LearningCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }

        public IEnumerable<Learning> GetLearning(Guid userId)
        {
            var sqlCheck = "SELECT * FROM Learning c WHERE c.UserId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", userId);

            var res = _dbContext.Connection.Query<Learning>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }

        public IEnumerable<Learning> GetLearningVideo(Guid videoId)
        {
            var sqlCheck = "SELECT * FROM Learning c WHERE c.VideoId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", videoId);

            var res = _dbContext.Connection.Query<Learning>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
