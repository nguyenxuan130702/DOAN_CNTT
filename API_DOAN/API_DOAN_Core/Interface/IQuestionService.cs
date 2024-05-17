using API_DOAN_Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IQuestionService:IBaseService<Question>
    {
        /// <summary>
        /// Hàm import file excel. 
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns></returns>
        IEnumerable<Question> ImportEmployee(IFormFile excelFile); 
    }
}
