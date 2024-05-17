using API_DOAN_Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class New
    {
        public Guid NewId { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_NewCode)]
        public string NewCode { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_NewTitle)]
        public string NewTitle { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_NewDate)]
        public DateTime NewDate { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_NewImage)]
        public string NewImage { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_NewContent)]
        public string NewContent { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
