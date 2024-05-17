using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IVideo_CourseRepository : IBaseRepository<Video_Course>
    {
        bool CheckDuplicateCode(string code);
    }
}
