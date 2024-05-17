using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class VideoCourseRepository : BaseRepository<VideoCourse>, IVideoCourseRepository, IDisposable
    {
        public VideoCourseRepository(IDOANDbContext dbContext) : base(dbContext)
        {
        }
        public void Dispose()
        {
            _dbContext.Connection.Dispose();
        }
    }
}
