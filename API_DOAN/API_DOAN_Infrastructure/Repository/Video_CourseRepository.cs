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
    public class Video_CourseRepository : BaseRepository<Video_Course>, IVideo_CourseRepository, IDisposable
    {
        public Video_CourseRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }
        public bool CheckDuplicateCode(string code)
        {
            var sqlCheck = "SELECT Video_CourseCode FROM Video_Course v WHERE v.Video_CourseCode = @code";
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
    }
}
