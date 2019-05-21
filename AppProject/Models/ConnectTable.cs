using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class ConnectTable
    {
        //many to many
        public int ProductesId { get; set; }
        public Productes Productes { get; set; }

        public int SizeId { get; set; }
        public Sizes Size { get; set; }

        public int ColorId { get; set; }
        public Colors Color { get; set; }

        public int MartId { get; set; }
        public Mart Mart { get; set; }
    }
}
