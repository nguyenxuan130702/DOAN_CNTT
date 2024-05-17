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
    public class ScoreRepository : BaseRepository<Score>, IScoreRepository, IDisposable
    {
        public ScoreRepository(IDOANDbContext dbContext) : base(dbContext) { }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT ScoreCode FROM Score c WHERE c.ScoreCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res != null;
        }
        public void Dispose()
        {
            _dbContext.Connection.Dispose();
        }

        public IEnumerable<Score> GetScores(Guid userId, Guid lessonId)
        {
            var sqlCheck = "SELECT * FROM Score c WHERE c.UserId = @code1 AND c.LessonId = @code2";
            var parameters = new DynamicParameters();
            parameters.Add("@code1", userId);
            parameters.Add("@code2", lessonId); 

            var res = _dbContext.Connection.Query<Score>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res; 
        }

        public IEnumerable<Score> GetScoreUser(Guid userId)
        {
            var sqlCheck = "SELECT * FROM Score c WHERE c.UserId = @code1";
            var parameters = new DynamicParameters();
            parameters.Add("@code1", userId);

            var res = _dbContext.Connection.Query<Score>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            return res;
        }
    }
}
