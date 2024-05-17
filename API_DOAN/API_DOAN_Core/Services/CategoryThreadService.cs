using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class CategoryThreadService : BaseService<CategoryThread>, ICategoryThreadService
    {
        ICategoryThreadRepository _categoryThreadRepository;
        public CategoryThreadService(ICategoryThreadRepository categoryThreadRepository) : base(categoryThreadRepository)
        {
            _categoryThreadRepository = categoryThreadRepository;
        }
    }
}
