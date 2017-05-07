using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ValidationAttributes
{

    public class 商品名稱需要包含WillAttribute : DataTypeAttribute
    {
        public 商品名稱需要包含WillAttribute() : base(DataType.Text)
        {

        }
        public override bool IsValid(object value)
        {
            var str = value.ToString();
            return str.Contains("Will");
        }


    }
}