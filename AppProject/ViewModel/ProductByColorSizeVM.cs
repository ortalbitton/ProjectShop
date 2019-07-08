using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.ViewModel
{
    public class ProductByColorSizeVM
    {

            //colorid or size id תלוי מה המשתמש בוחר
            public int Id { get; set; }

            public string ProductName { get; set; }

            public string ImgId { get; set; }

            public double Price { get; set; }

    
    }
}
