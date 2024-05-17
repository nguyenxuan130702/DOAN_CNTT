using API_DOAN_Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IVideo_QuestionService : IBaseService<Video_Question>
    {
        /// <summary>
        /// Hàm import file excel. 
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns></returns>
        IEnumerable<Video_Question> ImportEmployee(IFormFile excelFile);
    }
}
