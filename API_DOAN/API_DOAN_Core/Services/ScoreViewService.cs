using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class ScoreViewService : BaseService<ScoreView>, IScoreViewService
    {
        IScoreViewRepository _scoreRepository;
        public ScoreViewService(IScoreViewRepository scoreRepository) : base(scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }
    }
}
