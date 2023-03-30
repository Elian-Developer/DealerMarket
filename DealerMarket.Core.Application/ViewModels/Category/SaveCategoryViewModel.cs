using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.ViewModels.Category
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must put category Name.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You must put description category.")]
        public string? Description { get; set; }
    }
}
