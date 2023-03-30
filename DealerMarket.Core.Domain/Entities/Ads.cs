using DealerMarket.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Domain.Entities
{
    public class Ads : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public int? Price { get; set; }

        //Foreign Key
        public int CategoryId { get; set; }
        public string? UserId { get; set; }

       //Navigation Property
        public Category? Category { get; set; }


    }
}
