using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

        public ICollection<Quantities> DetailsClient { get; set; }


    }
}
