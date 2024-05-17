using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        public bool CheckDuplicateValidate(string code);
        public Teacher GetTeacherByEmail(string userEmail);
    }
}
