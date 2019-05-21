using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubName { get; set; }

        public Categories Categories { get; set; }

        public ICollection<Productes> Products { get; set; }
    }
}
