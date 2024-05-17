using API_DOAN_Core.Const;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class Radio
    {
        public Guid RadioId { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_RadioCode)]
        public string RadioCode { get; set; }
        public string? RadioName { get; set; }
        public string? RadioTacGia { get; set; }

        [Required(ErrorMessage = DOANConst.ERROR_RadioLink)]
        public string RadioLink { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
