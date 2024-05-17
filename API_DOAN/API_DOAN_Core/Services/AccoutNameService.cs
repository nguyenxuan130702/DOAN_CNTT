using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class AccoutNameService : BaseService<accoutname>, IVAccoutNameService
    {
        IVAccoutNameRepository _accoutRepository;
        public AccoutNameService(IVAccoutNameRepository accoutRepository) : base(accoutRepository)
        {
            _accoutRepository = accoutRepository;
        }

        public IEnumerable<accoutname> getlist(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
