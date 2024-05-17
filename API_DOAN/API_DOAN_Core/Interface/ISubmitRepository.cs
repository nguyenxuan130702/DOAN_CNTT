using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ISubmitRepository : IBaseRepository<Submit>
    {
        public bool CheckDuplicateCode(string code);
        IEnumerable<Submit> GetByUserAndAssignment(Guid userId, Guid assignmentId);
        IEnumerable<Submit> GetByUser(Guid userId); 

        IEnumerable<Submit> GetByAss(Guid assignmentId);
    }
}
