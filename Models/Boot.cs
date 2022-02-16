using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negrea_Georgiana_MasterProiect.Models
{
    public class Boot
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
            public ICollection<Order> Orders { get; set; }
        public ICollection<SoldBoot> SoldBoots { get; set; }

    }
    }

