using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IScoreRepository : IBaseRepository<Score>
    {
        public bool CheckDuplicateCode(string code);

        public IEnumerable<Score> GetScores(Guid userId, Guid lessonId);
        public IEnumerable<Score> GetScoreUser(Guid userId); 
    }
}
