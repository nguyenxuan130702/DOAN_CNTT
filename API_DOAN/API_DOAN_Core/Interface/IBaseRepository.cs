using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Hàm lấy toàn bộ danh sách 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Hàm lấy theo mã 
        /// </summary>
        /// <param name="code">Mã entity</param>
        /// <returns></returns>
        T? Get(string code);
        /// <summary>
        /// Hàm thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns></returns>
        int Insert(T entity); 
        /// <summary>
        /// Hàm sửa 1 đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns></returns>
        int Update(T entity);
        /// <summary>
        /// Hàm xóa 1 đối tượng
        /// </summary>
        /// <param name="code">Đối tượng</param>
        /// <returns></returns>
        int Delete(string code);
        int DeleteAny(string[] codes);
        IEnumerable<T> GetPage(int page, int pageSize); 
    }
}
