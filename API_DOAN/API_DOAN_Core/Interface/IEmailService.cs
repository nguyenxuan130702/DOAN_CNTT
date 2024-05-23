using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string email, string resetLink);
    }
}
