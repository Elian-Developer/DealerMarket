using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.Services;
using DealerMarket.Core.Application.ViewModels.Ads;
using DealerMarket.Models;
using DealerMarket.WebApp.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DealerMarket.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAdsService _adsServices;
        private readonly ICategoryService _categoryService;

        public HomeController(IAdsService adsServices, ICategoryService categoryService)
        {
            _adsServices = adsServices;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(FiltersAdsViewModels vm)
        {
            ViewBag.Categories = await _categoryService.GetAllViewModel();
            return View(await _adsServices.GetAllViewModelWithFilter(vm));
        }

        public async Task<IActionResult> Details(int id)
        {

            //await _adsService.GetDetailsAdsViewModel(id);

            return View(await _adsServices.GetDetailsAdsViewModel(id));
        }

    }
}