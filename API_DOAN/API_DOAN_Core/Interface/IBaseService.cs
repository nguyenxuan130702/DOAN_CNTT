using API_DOAN_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Hàm thêm mới bản ghi trả về trạng thái
        /// </summary>
        /// <param name="service"></param>
        /// <returns>trạng thái</returns>
        DOANServiceResult InsertService(T service);
        /// <summary>
        /// Hàm update trả về trạng thái
        /// </summary>
        /// <param name="service">1 đối tượng</param>
        /// <returns>trạng thái</returns>
        DOANServiceResult UpdateService(T service);
        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="code"></param>
        /// <returns>trạng thái</returns>
        DOANServiceResult DeleteService(string code);
        DOANServiceResult DeleteAnyService(string[] codes);
        ///Phân trang cho các object 
        DOANServiceResult Page(int page, int pageSize); 
    }
}
