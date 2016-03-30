using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class OrderItem
    {
        public Article Article { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return this.Quantity * Article.Price;
            }
        }
    }
}
