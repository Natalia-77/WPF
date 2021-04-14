using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatShop.Application.Interfaces
{
    //public delegate void AddCatdelegate(int count);
    public delegate void AddCatdelegate(int i);
    public interface ICatService
    {
        event AddCatdelegate AddNewCatItem;

        bool IsCancelled { get; set; }
        Task InsertCatsAsync(int count, ManualResetEvent mrse);
        int Count();
        /// <summary>
        /// Add Cat item to database.
        /// </summary>
        /// <param name="kol"></param>
        void AddCat(int count, ManualResetEvent mrse);

        
    }
}
