using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Productes
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ICollection<ConectTable> Details { get; set; }
        public double Price { get; set; }
        public int AmountInStock { get; set; }
        public int AmountOfOrders { get; set; }
        public double DeliveryPrice { get; set; }
        public string ImgId { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }


    }
}
