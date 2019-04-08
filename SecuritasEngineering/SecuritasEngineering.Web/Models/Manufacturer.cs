using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecuritasEngineering.Web.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }

        [MaxLength(200)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}