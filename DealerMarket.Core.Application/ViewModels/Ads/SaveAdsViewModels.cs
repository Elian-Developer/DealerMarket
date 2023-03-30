using DealerMarket.Core.Application.ViewModels.Category;
using DealerMarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.ViewModels.Ads
{
    public class SaveAdsViewModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must put the Ads Name.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You must put the description ads, no more than 100 characters.")]
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "You must put the Ads price.")]
        public int? Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You must select the Ads category.")]
        public int CategoryId { get; set; }
        public string? UserId { get; set; }
        public List<CategoryViewModel>? categories { get; set; }
        public IFormFile? File { get; set; }


    }
}
