using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class SearchRtn
    {
        public SearchRtn()
        {
            this.price = 1;
            this.stock = 200;
        }
        public string q { get; set; }
        public int stock { get; set; }
        public int price { get; set; }

    }
}