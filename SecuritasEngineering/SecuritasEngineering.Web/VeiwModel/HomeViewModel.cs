using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecuritasEngineering.Web.Models;

namespace SecuritasEngineering.Web.VeiwModel
{
    public class HomeViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers  { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}