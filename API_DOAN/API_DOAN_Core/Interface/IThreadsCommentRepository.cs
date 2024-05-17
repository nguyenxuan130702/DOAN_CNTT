using API_DOAN_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IThreadsCommentRepository : IBaseRepository<ThreadsComment>
    {
        /// <summary>
        /// Hàm kiểm tra trùng mã
        /// </summary>
        /// <param name="code">Mã cần kiểm tra</param>
        /// <returns>true là đã tồn tại, false là chưa tồn tại</returns>
        bool CheckDuplicateCode(string code);
        /// Hàm lấy ra danh sách các thread thuộc vào category 
        public IEnumerable<ThreadsComment> GetCategory(Guid categoryId);
        ///hàm lấy ra thông tin của thread theo id 
        public ThreadsComment GetThread(Guid threadId);
    }
}
