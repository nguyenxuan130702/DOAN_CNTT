using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        public bool CheckDuplicateCode(string code);
        public Result GetByUser(Guid userId, Guid assignmentId);
        public Result GetByAss(Guid assignmentId); 
    }
}
