using Bogus;
using CatShop.Application.Interfaces;
using CatShop.Domain;
using CatShop.EFData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CatShop.Infrastructure.Service
{
    public class CatService : ICatService
    {
        private readonly EFContext context;

       
        public bool IsCancelled { get; set; }

        public CatService()
        {
            context = new EFContext();
        }
        public event AddCatdelegate AddNewCatItem;

        /// <summary>
        /// Додавання котів в базу Богусом.
        /// </summary>
        public void AddCat(int count, ManualResetEvent mrse)
        {
            //Ліст згенерованих даних Богусом.
            List<AppCat> cat = new List<AppCat>();

            //Новий створений Ліст,куди будуть додаватись коти в процесі додавання в БД(але без запису в БД!).
            // List<AppCat> check = new List<AppCat>();

            //Змінні необхідні для заповнення Богусом.
            DateTime start = new DateTime(1998, 03, 05);
            DateTime end = new DateTime(2020, 12, 30);

            //Знаходжу найбільший Айді кота,тобто останній доданий.
            int idvalue = context.Cats.Max(z => z.Id);

            //Збільшую на 1,щоб уникнути повтора праймері кі,тобто щоб не було виключення.
            int newval = idvalue + 1;

            //Генерація даних Богусом.
            var catss = new Faker<AppCat>("uk")
                .RuleFor(x => x.Id, f => newval++)
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.Birth, f => f.Date.Between(start, end))
                .RuleFor(x => x.Description, f => f.Lorem.Text())
                .RuleFor(x => x.Gender, f => f.Person.Gender.ToString());


            //додавання в Ліст згенероаних даних,відповідно до вказаної кількості.
            for (int i = 0; i < count; i++)
            {
                cat.Add(catss.Generate());
            }

           
                //початок відліку часу,витраченого на додавання котів.
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
            using var transaction = context.Database.BeginTransaction();
            {
                try
                {

                    for (int j = 0; j < cat.Count; j++)
                    {
                    mrse.WaitOne();

                    //якщо натиснута кнопка СТОП,виходимо і кнопка стане не активна.
                    if (IsCancelled)
                    {
                        break;
                    }

                    //Додавання котів в БД.
                    AppCat appCat = new AppCat
                    {
                        Id = cat[j].Id,
                        Name = cat[j].Name,
                        Birth = cat[j].Birth,
                        Description = cat[j].Description,
                        Gender = cat[j].Gender,
                        Image = cat[j].Image
                    };
                        context.Cats.Add(appCat);
                        context.SaveChanges();
                         //кожен доданий кіт додається в цей створений Ліст.
                        // check.Add(appCat);

                        AddNewCatItem?.Invoke(j + 1);
                        Debug.WriteLine("Insert cat " + appCat.Id);
                        //Debug.WriteLine("check-" + check.Count);
                        //Debug.WriteLine("cat-" + count);
                        Thread.Sleep(2000);
                }

                //Перевірка кількості елементів в Лісті з котами і вказаною для додавання кількістю котів.            
                //Debug.WriteLine("check-" + check.Count);
                Debug.WriteLine("cat-" + count);

                //Якщо вони будуть рівні,то коти з Ліст чек,будуть записані в базу.
                //if (check.Count == cat.Count)
                //{
                //    foreach (var item in check)
                //    {
                //        //Якщо вони рівні,тобто в Ліст потрапила та кількість котів,що ми хотіли додати,
                //        //то кожен з них записується і зберігається в БД.
                //        context.Cats.Add(item);
                //        context.SaveChanges();
                //    }
                //}


                //Кінець підрахунку часу,витраченого на додавання.
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                //Отримали час.
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                //Вивели.
                Debug.WriteLine("Час додавання котів: " + elapsedTime);

                    if (!IsCancelled)
                    {                        
                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                }

            }
        }

        public Task InsertCatsAsync(int count, ManualResetEvent mrse)
        {
            return Task.Run(() => AddCat(count,mrse));            
        }

        public int Count() => context.Cats.Count();
        
    }
}
