using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CreditCard { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public Mart Mart { get; set; }

    }
}
