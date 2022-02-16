using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negrea_Georgiana_MasterProiect.Models
{
    public class SoldBoot
    {
        public int SellerID { get; set; }
        public int BootID { get; set; }
        public Seller Seller { get; set; }
        public Boot Boot { get; set; }
    }
}
