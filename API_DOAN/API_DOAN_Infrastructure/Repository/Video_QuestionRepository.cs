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
    public class Video_QuestionRepository : BaseRepository<Video_Question>, IVideo_QuestionRepository, IDisposable
    {
        public Video_QuestionRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT Video_QuestionCode FROM Video_Question v WHERE v.Video_QuestionCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            //throw new NotImplementedException();
            return res != null;
        }

        public IEnumerable<Video_Question> GetQuestionVideo(Guid videoId)
        {
            var sqlCheck = "SELECT * FROM Video_Question v WHERE v.VideoId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", videoId);

            var res = _dbContext.Connection.Query<Video_Question>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            //throw new NotImplementedException();
            return res;
        }
    }
}
