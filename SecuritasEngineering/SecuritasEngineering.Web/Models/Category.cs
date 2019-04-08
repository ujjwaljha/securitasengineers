using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecuritasEngineering.Web.Models
{
    public class Category
    {
        public int ID { get; set; }

        [MaxLength(200)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Upload Image")]
        public string ImageURL { get; set; }

    }
}