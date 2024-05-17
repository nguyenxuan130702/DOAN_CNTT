using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using Dapper;

namespace API_DOAN_Infrastructure.Repository
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository, IDisposable
    {
        public VideoRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT VideoCode FROM Video v WHERE v.VideoCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);

            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            //throw new NotImplementedException();
            return res != null; 
        }
        public void Dispose()
        {
            _dbContext.Connection.Dispose();
        }

        public IEnumerable<Video> GetVideo_Course(Guid video_CourseId)
        {
            var sqlCheck = "SELECT * FROM Video v WHERE v.Video_CourseId = @code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", video_CourseId);

            var res = _dbContext.Connection.Query<Video>(sqlCheck, parameters, transaction: _dbContext.Transaction);
            //throw new NotImplementedException();
            return res;
        }
    }
}
