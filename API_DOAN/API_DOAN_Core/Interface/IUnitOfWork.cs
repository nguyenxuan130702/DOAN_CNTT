using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        INewRepository News { get; }
        IQuestionRepository Questions { get; }
        IVideo_QuestionRepository Video_Question { get; }
        void BeginTransaction();
        void Commit();
        void Roolback(); 
    }
}
