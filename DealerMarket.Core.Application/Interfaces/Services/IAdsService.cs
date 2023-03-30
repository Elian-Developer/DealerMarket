using DealerMarket.Core.Application.ViewModels.Ads;
using DealerMarket.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.Interfaces.Services
{
    public interface IAdsService : IGenericService<SaveAdsViewModels, AdsViewModels, Ads>
    {
        Task<List<AdsViewModels>> GetAllViewModelWithFilter(FiltersAdsViewModels filters);
        Task<List<AdsViewModels>> GetAllWithInclude();
        Task<AdsViewModels> GetDetailsAdsViewModel(int id);
    }
}
