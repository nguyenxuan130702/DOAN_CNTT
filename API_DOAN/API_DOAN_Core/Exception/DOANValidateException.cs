using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Exception
{
    public class DOANValidateException:System.Exception
    {
        private string MsgError = string.Empty;
        public DOANValidateException(string error)
        {
            this.MsgError = error;
        }
        public override string Message => this.MsgError;
    }
}
