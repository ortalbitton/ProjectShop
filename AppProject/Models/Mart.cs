using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Mart
    {
        [Key]
        public int MartId { get; set; }
        //need to add list of products id
        public ICollection<ConectTable> Details { get; set; }

        /* The ScreenId property helps the framework to
         understand the relation*/

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
