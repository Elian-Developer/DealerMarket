using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealerMarket.WebApp.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
         
            SaveCategoryViewModel vm = new SaveCategoryViewModel();
            return View("SaveCategory", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Add(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdSaveViewModel(id);
            return View("SaveCategory", category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm, int id)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Edit(vm, id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdSaveViewModel(id);
            return View("Delete", category);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
