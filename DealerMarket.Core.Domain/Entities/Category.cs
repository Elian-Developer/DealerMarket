using DealerMarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        //Navigation Property
        public ICollection<Ads>? Ads { get; set; }
    }
}
