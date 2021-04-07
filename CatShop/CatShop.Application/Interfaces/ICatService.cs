using System;
using System.Collections.Generic;
using System.Text;

namespace CatShop.Application.Interfaces
{
    //public delegate void AddCatdelegate(int count);
    public delegate void AddCatdelegate();
    public interface ICatService
    {
        event AddCatdelegate AddNewCatItem;

        int Count();
        /// <summary>
        /// Add Cat item to database.
        /// </summary>
        /// <param name="kol"></param>
        void AddCat();
    }
}
