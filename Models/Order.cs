using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negrea_Georgiana_MasterProiect.Models
{
    public class Order
    {

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int BootID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Boot Boot { get; set; }
    }
}
