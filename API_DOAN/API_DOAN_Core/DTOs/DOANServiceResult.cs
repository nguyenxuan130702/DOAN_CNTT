using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.DTOs
{
    public class DOANServiceResult
    {
        public bool Success { get; set; }
        public string? userMsg { get; set; }
        public string? devMsg { get; set; }
        public object? Data {  get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
