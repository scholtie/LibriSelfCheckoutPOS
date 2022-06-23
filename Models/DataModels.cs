using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibriSelfCheckoutPOS.Models
{
    public class DataModels
    {
        public class ScannedProduct
        {
            public int productId { get; set; }
            public string productName { get; set; }
            public int productArticleNumber { get; set; }
            public double productPrice { get; set; }
            public double productUnitPrice { get; set; }
            public double productDiscount { get; set; }
            public Boolean productIsDeleted { get; set; }
            public override string ToString()
            {
                return productId + productName + ", " + productArticleNumber + ", " + productPrice + ", " + productUnitPrice + ", " + productDiscount + ", " + productIsDeleted;
            }

        }

        public class User
        {
            public string Name { get; set; }
            public int Permission { get; set; }
            public bool IsAdmin { get; set; }

            public override string ToString()
            {
                return Name + ", " + Permission + ", " + IsAdmin;
            }
        }

        public class AllProducts
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ArticleNumber { get; set; }
            public double Price { get; set; }
            public double UnitPrice { get; set; }
            public double Discount { get; set; }

            public override string ToString()
            {
                return Id + ", " + Name + ", " + ArticleNumber + Price + UnitPrice + Discount;
            }
        }
    }
}
