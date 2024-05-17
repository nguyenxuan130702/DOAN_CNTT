using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IVideoRepository:IBaseRepository<Video>
    {
        bool CheckDuplicateCode(string code);
        IEnumerable<Video> GetVideo_Course(Guid video_CourseId); 
    }
}
