using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Mart
    {

        public int Id { get; set; }

        public Customer Customer { get; set; }

        public ICollection<ConnectTable> Details { get; set; } 
    }
}
