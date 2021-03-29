using CatShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatShop.EFData
{
    public class DBSeeder
    {
        public static void SeedData(EFContext context)
        {
            if (context.Cats.Count() == 0)
            {
                context.Cats
                    .Add(new AppCat
                    {
                        Name = "Марсік",
                        Birth=new DateTime(2019,12,5),
                        Description="Рудий,пухнастий,зелені очі.Характер лагідний.",
                        Gender="Котик"

                    });
                context.SaveChanges();
            }

            if(context.CatPrices.Count()==0)
            {
                context.CatPrices.Add(new AppCatPrice
                {
                    Price=330.22M,
                    DateCreate=new DateTime(2021,02,01),
                    CatId=8

                });
                context.SaveChanges();
            }
        }
    }
}
