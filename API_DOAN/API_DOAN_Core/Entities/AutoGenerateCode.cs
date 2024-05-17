using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Entities
{
    public class AutoGenerateCode
    {
        private int lastGeneratedCode = 50;

        // Hàm tạo mã tự động
        public string GenerateCode(int length)
        {
            // Các ký tự được sử dụng để tạo mã
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Tạo một đối tượng Random
            Random random = new Random();

            // Tạo mã từ các ký tự ngẫu nhiên
            string code = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return code;
        }

    }
}
