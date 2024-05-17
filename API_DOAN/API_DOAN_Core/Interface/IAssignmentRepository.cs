using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IAssignmentRepository : IBaseRepository<Assignment>
    {
        bool CheckDuplicateCode(string code);
        IEnumerable<Assignment> GetByClass(Guid classId);
    }
}
