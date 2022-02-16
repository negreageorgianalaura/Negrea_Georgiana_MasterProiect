using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negrea_Georgiana_MasterProiect.Models;

namespace Negrea_Georgiana_MasterProiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();
            if (context.Boots.Any())
            {
                return; // BD a fost creata anterior
            }
            var boots = new Boot[]
            {
         new Boot{Name="Bocanci",Brand="Karl Lagerfeld",Price=Decimal.Parse("20000")},
         new Boot{Name="Cizme piele",Brand="Dr Martens",Price=Decimal.Parse("1800")},
         new Boot{Name="Moonboot",Brand="Guess",Price=Decimal.Parse("1500")},
         new Boot{Name="Cizme lac",Brand="Tommy Jeans",Price=Decimal.Parse("780")},
         new Boot{Name="Cizme scurte",Brand="Calvin Klein",Price=Decimal.Parse("950")},
         new Boot{Name="Cizme guma",Brand="Buffalo",Price=Decimal.Parse("1600")},
            };
            foreach (Boot s in boots)
            {
                context.Boots.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

                 new Customer{CustomerID=100,Name="Popescu Luciana Maria",BirthDate=DateTime.Parse("1989-09-01")},
                 new Customer{CustomerID=101,Name="Popovici Adrian",BirthDate=DateTime.Parse("1969-07-08") },
                 new Customer{CustomerID=102,Name="Stan Alexandru",BirthDate=DateTime.Parse("1992-02-18")},
                 new Customer{CustomerID=103,Name="Parker Cristina",BirthDate=DateTime.Parse("1983-11-02")},
                 new Customer{CustomerID=104,Name="Lucu Georgia",BirthDate=DateTime.Parse("1981-10-12")}

            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
             new Order{BootID=1,CustomerID=104,OrderDate=DateTime.Parse("02-25-2020")},
             new Order{BootID=3,CustomerID=102,OrderDate=DateTime.Parse("09-28-2020")},
             new Order{BootID=4,CustomerID=103,OrderDate=DateTime.Parse("10-28-2020")},
             new Order{BootID=2,CustomerID=101,OrderDate=DateTime.Parse("09-28-2020")},
             new Order{BootID=5,CustomerID=100,OrderDate=DateTime.Parse("09-28-2020")}
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
            var sellers = new Seller[]
             {

 new Seller{SellerName="Office Shoes",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
 new Seller{SellerName="Deichman",Adress="Str. Plopilor, nr. 35,Ploiesti"},
 new Seller{SellerName="CCC",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
             };
            foreach (Seller p in sellers)
            {
                context.Sellers.Add(p);
            }
            context.SaveChanges();
            var soldboots = new SoldBoot[]
            {
 new SoldBoot {
 BootID = boots.Single(c => c.Name == "Bocanci" ).ID,SellerID = sellers.Single(i => i.SellerName =="Office Shoes").ID},
 new SoldBoot 
 {BootID = boots.Single(c => c.Name == "Cizme piele" ).ID,SellerID = sellers.Single(i => i.SellerName =="Office Shoes").ID},
 new SoldBoot {
 BootID = boots.Single(c => c.Name == "Moonbot" ).ID, SellerID = sellers.Single(i => i.SellerName =="Deichman").ID},
 new SoldBoot {
 BootID = boots.Single(c => c.Name == "Cizme lac" ).ID,SellerID = sellers.Single(i => i.SellerName == "CCC").ID},
 new SoldBoot {
 BootID = boots.Single(c => c.Name == "Cizme scurte" ).ID,SellerID = sellers.Single(i => i.SellerName == "Deichman").ID},
 new SoldBoot {
 BootID = boots.Single(c => c.Name == "Cizme guma" ).ID, SellerID = sellers.Single(i => i.SellerName == "CCC").ID},
            };
            foreach (SoldBoot pb in soldboots)
            {
                context.SoldBoots.Add(pb);
            }
            context.SaveChanges();
        }
    }
}