using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace CatShop.Application
{
    public class CatValidator: AbstractValidator<ViewModelCats>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.Name)
                .Must(BeAValidName)                
                .WithMessage("Could not be empty!!");       
                     
            RuleFor(cat=>cat.Price)
                .Must(BeValidPrice)
                .WithMessage("Could not be zero");

            RuleFor(cat => cat.Birthday)
                .Must(BeValidDate)
                .WithMessage("Invalad data");


        }

        private static bool BeAValidName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //var regex = new Regex(@"/[a-zA-Z]+$/");
                var regex = new Regex(@"^(?:([А-Яа-я])(?!\1))*$");
                return regex.IsMatch(name);
            }
            return false;
        }

        private static bool BeValidPrice(decimal price)
        {
            if (price>0)
            {
                var regex = new Regex(@"\d+(\.\d{1,2})?");
                return regex.IsMatch(price.ToString());
            }
            return false;
        }
        private static bool BeValidDate(DateTime date)
        {
            int current = DateTime.Now.Year;
            int testyear = date.Year;
            if(testyear<=current)
            {
                return true;
            }
            return false;

        }



    }
}
