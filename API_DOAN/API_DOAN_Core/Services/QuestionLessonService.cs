using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class QuestionLessonService : BaseService<QuestionLesson>, IQuestionLessonService
    {
        IQuestionLessonRepository _questionLessonRepository;
        public QuestionLessonService(IQuestionLessonRepository questionLessonRepository) : base(questionLessonRepository)
        {
            _questionLessonRepository = questionLessonRepository;
        }
    }
}
