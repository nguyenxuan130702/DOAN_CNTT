using API_DOAN_Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DOAN_Core.Validation; 

namespace API_DOAN_Core.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_UserName)]
        public string UserName { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_UserAccout)]
        public string UserAccout { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_UserEmail)]
        [EmailAddress(ErrorMessage = DOANConst.ERROR_EmailValidate)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_UserPassword)]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_UserType)]
        public int UserType { get; set; }

        public string? UserImage { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set;}
    }
}
