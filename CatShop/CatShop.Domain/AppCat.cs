using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatShop.Domain
{
    [Table("tblCats")]
    public class AppCat
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime Birth { get; set; }

        [Required, StringLength(4000)]
        public string Description { get; set; }

        public string Gender { get; set; }

        [StringLength(255)]
        public string Image { get; set; }
        public virtual ICollection<AppCatPrice> AppCatPrices { get; set; }
    }
}
