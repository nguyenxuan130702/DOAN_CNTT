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
    public class CourseDaiSoRepository : BaseRepository<CourseDaiSo>, ICourseDaiSoRepository, IDisposable
    {
        public CourseDaiSoRepository(IDOANDbContext dbContext) : base(dbContext) { }
    }
}
