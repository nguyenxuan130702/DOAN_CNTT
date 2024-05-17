using API_DOAN_Core.Interface;
using API_DOAN_Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        IDOANDbContext _dbContext;
        
        public UnitOfWork(IDOANDbContext dbContext, INewRepository newRepository, IQuestionRepository questionRepository, IVideo_QuestionRepository video_QuestionRepository)
        {
            _dbContext = dbContext;
            News = newRepository; 
            Questions = questionRepository;
            Video_Question = video_QuestionRepository;
        }
        public INewRepository News { get; }

        public IQuestionRepository Questions { get; }
        public IVideo_QuestionRepository Video_Question { get; }

        public void BeginTransaction()
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Transaction.Commit();
        }

        public void Dispose()
        {
            _dbContext.Connection.Close();
        }

        public void Roolback()
        {
            _dbContext.Connection.Dispose();
        }
    }
}
