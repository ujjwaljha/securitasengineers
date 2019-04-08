using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecuritasEngineering.Web.Models;

namespace SecuritasEngineering.Web.VeiwModel
{
    public class ProductViewModel
    {
        public List<_ProductViewModel> Manufacturers { get; set; }
        public Product Product { get; set; }
        public List<_ProductViewModel> Categories { get; set; }
    }


    public class _ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean IsChecked { get; set; }
    }
}