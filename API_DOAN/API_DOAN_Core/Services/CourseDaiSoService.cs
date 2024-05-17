﻿using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class CourseDaiSoService : BaseService<CourseDaiSo>, ICourseDaiSoService
    {
        ICourseDaiSoRepository _courseDaiSoRepository;
        public CourseDaiSoService(ICourseDaiSoRepository courseRepository) : base(courseRepository)
        {
            _courseDaiSoRepository = courseRepository;
        }
    }
}
