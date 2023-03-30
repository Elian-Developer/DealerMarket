using DealerMarket.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.ViewModels.Ads
{
    public class AdsViewModels
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public int? Price { get; set; }
        public int CategoryId { get; set; }
        public string? UserId { get; set; }
        public string? CategoryName { get; set; }
        public string? AdsUserName { get; set; }
        public DateTime? AdsCreated { get; set; }
        public string? AdsUserEmail { get; set; }
        public string? AdsUserPhone { get; set; }
        public DateTime? AdsLastModified { get; set; }

        public CategoryViewModel Category { get; set; }

    }
}
