using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
