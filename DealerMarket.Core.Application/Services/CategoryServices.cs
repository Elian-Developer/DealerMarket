using AutoMapper;
using DealerMarket.Core.Application.Interfaces.Repositories;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.ViewModels.Category;
using DealerMarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.Services
{
    public class CategoryServices : GenericServices<SaveCategoryViewModel, CategoryViewModel, Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }


    }
}
