using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Colors
    {
        public int Id { get; set; }
        public string ColorName { get; set; }

        public ICollection<ConnectTable> DetailsManager { get; set; }

        public ICollection<Quantities> DetailsClient { get; set; }
    }
}
