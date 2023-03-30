using DealerMarket.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using DealerMarket.Core.Application.ViewModels.Ads;
using Microsoft.AspNetCore.Authorization;

namespace DealerMarket.WebApp.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {
        private readonly IAdsService _adsService;
        private readonly ICategoryService _categoryService;

        public AdsController(IAdsService adsService, ICategoryService categoryService)
        {
            _adsService = adsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _adsService.GetAllWithInclude());
        }

        public async Task<IActionResult> Create()
        {
            SaveAdsViewModels vm = new();
            vm.categories = await _categoryService.GetAllViewModel();
            return View("SaveAds", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdsViewModels vm)
        {
            if (!ModelState.IsValid) //Here we are doing Validations
            {
                vm.categories = await _categoryService.GetAllViewModel();
                return View("SaveAds", vm);
            }

            // Process To upload file in the app.
            SaveAdsViewModels adsVm = await _adsService.Add(vm);
            if(adsVm != null && adsVm.Id != 0)
            {
                adsVm.ImageURL = UploadFile(vm.File, adsVm.Id);
                await _adsService.Edit(adsVm, adsVm.Id);

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveAdsViewModels vm = await _adsService.GetByIdSaveViewModel(id);
            vm.categories = await _categoryService.GetAllViewModel();
            return View("SaveAds", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdsViewModels vm, int id)
        {
       
            if (!ModelState.IsValid) //Here we are doing Validations
            {
                vm.categories = await _categoryService.GetAllViewModel();
                return View("SaveAds", vm);
            }
            //Process to Edit File and change for the previous
            SaveAdsViewModels adsVm = await _adsService.GetByIdSaveViewModel(vm.Id);
            vm.ImageURL = UploadFile(vm.File, adsVm.Id, true, adsVm.ImageURL);

            await _adsService.Edit(vm, id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ads = await _adsService.GetByIdSaveViewModel(id);
            return View("Delete", ads);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _adsService.Delete(id);

            //Process to remove File with their directory
            string basepath = $"/Images/Ads/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToAction("Index");
        }



        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imageUrl = "")
        {
            if(isEditMode && file == null)
            {
                return imageUrl;
            }

            //get directory path
            string basepath = $"/Images/Ads/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");

            //Create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string FileNameWithPath = Path.Combine(path, filename);

            using(var stream = new FileStream(FileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (isEditMode)
            {
                string[] oldImagePart = imageUrl.Split('/');
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basepath}/{filename}";
        }
    }
}
