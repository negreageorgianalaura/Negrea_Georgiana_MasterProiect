using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negrea_Georgiana_MasterProiect.Models.ShopViewModels
{
    public class SellerIndexData
    {
        public IEnumerable<Seller> Sellers { get; set; }
        public IEnumerable<Boot> Boots { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
