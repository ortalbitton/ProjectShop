using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Productes
    {
        public int Id { get; set; }
        public string ProductName { get; set; }      
        public double Price { get; set; }
        public int AmountInStock { get; set; }
        public int AmountOfOrders { get; set; }
        public double DeliveryPrice { get; set; }
        public string ImgId { get; set; }

        public ICollection<ConnectTable> Details { get; set; }

        public SubCategory SubCategory { get; set; }

    }
}
