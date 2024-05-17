using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ILearningRepository : IBaseRepository<Learning>
    {
        bool CheckDuplicateCode(string code);
        IEnumerable<Learning> GetLearning(Guid userId);
        IEnumerable<Learning> GetLearningVideo(Guid videoId); 
    }
}
