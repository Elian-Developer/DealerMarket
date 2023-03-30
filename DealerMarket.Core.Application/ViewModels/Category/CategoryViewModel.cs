﻿using DealerMarket.Core.Application.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int AdsQuantity { get; set; }
        public ICollection<AdsViewModels>? Ads { get; set; }
    }
}