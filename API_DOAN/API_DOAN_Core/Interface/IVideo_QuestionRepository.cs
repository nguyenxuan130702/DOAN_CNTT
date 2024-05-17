using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IVideo_QuestionRepository : IBaseRepository<Video_Question>
    {
        bool CheckDuplicateCode(string code);

        IEnumerable<Video_Question> GetQuestionVideo(Guid videoId); 
    }
}
