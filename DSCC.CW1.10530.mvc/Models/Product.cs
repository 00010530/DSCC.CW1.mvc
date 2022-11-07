using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCC.CW1._10530.mvc.Models
{
    public class Product    
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category ProductCategory { get; set; }

    }
}