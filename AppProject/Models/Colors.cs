using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Colors
    {
        [Key]
        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public ICollection<ConectTable> Details { get; set; }
    }
}
