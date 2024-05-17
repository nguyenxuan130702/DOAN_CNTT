using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Interface
{
    public interface IDOANDbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        /// <summary>
        /// Hàm lấy ra danh sách bản ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Get<T>();
        /// <summary>
        /// Hàm lấy ra 1 bản ghi theo mã
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        T? Get<T>(string code);
        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert<T>(T entity); 
        /// <summary>
        /// Hàm update bản ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update<T>(T entity);
        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete<T>(string code);
        int DeleteAny<T>(string[] codes); 

    }
}
