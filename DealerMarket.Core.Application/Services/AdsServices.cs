using AutoMapper;
using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Helpers;
using DealerMarket.Core.Application.Interfaces.Repositories;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.ViewModels.Ads;
using DealerMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.Services
{
    public class AdsServices : GenericServices<SaveAdsViewModels, AdsViewModels, Ads>, IAdsService
    {
        private readonly IAdsRepository _adsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel; //Aqui estamos utilizando el user de identity
        public AdsServices(IAdsRepository adsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(adsRepository, mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user"); //Aqui le indicamos que utilice las sesiones.
        }

        public override async Task<SaveAdsViewModels> Add(SaveAdsViewModels vm)
        {
            vm.UserId = _userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Edit(SaveAdsViewModels vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Edit(vm, id);
        }

        public async Task<List<AdsViewModels>> GetAllWithInclude()
        {
            var AdsList = await _adsRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return AdsList.Where(ads => ads.UserId == _userViewModel.Id).Select(Ads => new AdsViewModels
            {
                Id = Ads.Id,
                Name = Ads.Name,
                ImageURL = Ads.ImageURL,
                Description = Ads.Description,
                Price = Ads.Price,
                CategoryId = Ads.CategoryId,
                CategoryName = Ads.Category.Name,
                UserId = _userViewModel.Id
              

            }).ToList();
        }

        public async Task<List<AdsViewModels>> GetAllViewModelWithFilter(FiltersAdsViewModels filters)
        {
            var AddList = await _adsRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var ListViewModels = AddList.Where(ads => ads.UserId != _userViewModel.Id)
                .Select(Ads => new AdsViewModels
            {
                Id = Ads.Id,
                Name = Ads.Name,
                ImageURL = Ads.ImageURL,
                Description = Ads.Description,
                Price = Ads.Price,
                CategoryId = Ads.CategoryId,
                UserId = _userViewModel.Id,
                CategoryName = Ads.Category.Name,
                AdsUserName = Ads.CreatedBy,
                AdsUserEmail = _userViewModel.Email,
                AdsUserPhone = _userViewModel.Phone,
                AdsCreated = Ads.Created,
                AdsLastModified = Ads.LastModified,

                }).ToList();

            if(filters.AdsName != null)
            {
                ListViewModels = ListViewModels.Where(ads => ads.Name.Contains(filters.AdsName)).ToList();
            }

            else if(filters.CategoryId != null)
            {
                ListViewModels = ListViewModels.Where(cat => cat.CategoryId == filters.CategoryId.Value).ToList();
            }

            return ListViewModels;
        }

        public async Task<AdsViewModels> GetDetailsAdsViewModel(int id)
        {
            var ads = await _adsRepository.GetByIdWithIncludeAsync(id, new List<string> { "Category" });

            AdsViewModels vm = new()
            {
                Id = ads.Id,
                Name = ads.Name,
                Description = ads.Description,
                Price = ads.Price,
                ImageURL = ads.ImageURL,
                CategoryId = ads.CategoryId,
                UserId = ads.UserId,
                CategoryName = ads.Category.Name,
                AdsUserName = ads.LastModifiedBy,
                AdsCreated = ads.LastModified,
                AdsUserEmail = _userViewModel.Email,
                AdsUserPhone = _userViewModel.Phone
            };

            return vm;
        }

    }
}
