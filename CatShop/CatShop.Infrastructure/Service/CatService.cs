using Bogus;
using CatShop.Application.Interfaces;
using CatShop.Domain;
using CatShop.EFData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CatShop.Infrastructure.Service
{
    public class CatService : ICatService
    {
        private readonly EFContext context;
        public CatService()
        {
            context = new EFContext();
        }
        public event AddCatdelegate AddNewCatItem;

        /// <summary>
        /// Отримати множину айді котів,для завповнення в Богусі.
        /// </summary>
        /// <returns></returns>
        public List<int> GetIdCats()
        {
           
            List<int> IdList = new List<int>();
            var cats_list = context.Cats.Select(c => c.Id);
            foreach (var item in cats_list)
            {
                IdList.Add(item);
            }

            return IdList;
        }
        // public void AddCat(int kol)
        public void AddCat()
        {

            List<AppCat> cat = new List<AppCat>();
            List<AppCatPrice> price = new List<AppCatPrice>();
            Random rand = new Random();
            List<int> ID = GetIdCats();
            DateTime start = new DateTime(1998, 03, 05);
            DateTime end = new DateTime(2020, 12, 30);            


            var catss = new Faker<AppCat>("uk")
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x=>x.Birth,f=>f.Date.Between(start,end))
                .RuleFor(x=>x.Description,f=>f.Lorem.Text())
                .RuleFor(x => x.Gender, f => f.Person.Gender.ToString());

            var catsprice = new Faker<AppCatPrice>("uk")
                .RuleFor(x => x.Price, f => f.Random.Decimal(120, 1522))
                .RuleFor(x => x.DateCreate, f => f.Date.Between(start,end))               
                .RuleFor(x => x.CatId, f => ID[rand.Next(1, ID.Count() - 1)]);

            //for (int i = 0; i < kol; i++)
            //{
                cat.Add(catss.Generate());
            //}
           
            //for (int j = 0; j < kol; j++)
            //{
                price.Add(catsprice.Generate());
           // }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var res in cat)
            {
                context.Cats
                .Add(
                new AppCat
                {
                    Name = res.Name,
                    Birth = res.Birth,
                    Description = res.Description,
                    Gender = res.Gender,
                    

                }) ;
               
                
            }

            foreach (var item in price)
            {
                context.CatPrices
                    .Add(
                    new AppCatPrice
                    {
                        Price=item.Price,
                        DateCreate=item.DateCreate,
                        CatId=item.CatId
                    });

                Debug.WriteLine("Insert cat--> " + item.CatId);
                AddNewCatItem?.Invoke();
            }
            stopWatch.Stop();            
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("Час додавання котів: " + elapsedTime);

            //AddNewCatItem?.Invoke(price.Count);

            // AddNewCatItem?.Invoke(kol);
            //Debug.WriteLine("Insert cat " + Cats.Id);
            //context.SaveChanges();
        }

        public int Count() => context.Cats.Count();
        
    }
}
