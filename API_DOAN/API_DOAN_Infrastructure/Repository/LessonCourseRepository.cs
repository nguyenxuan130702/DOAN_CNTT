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
    public class LessonCourseRepository : BaseRepository<LessonCourse>, ILessonCourseRepository, IDisposable
    {
        public LessonCourseRepository(IDOANDbContext dbContext) : base(dbContext) { }
        public bool CheckDuplicateCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
