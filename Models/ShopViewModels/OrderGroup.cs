using System;
using System.ComponentModel.DataAnnotations;
namespace Negrea_Georgiana_MasterProiect.Models.ShopViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int BootCount { get; set; }

    }
}