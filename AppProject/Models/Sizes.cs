﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class Sizes
    {
        public int Id { get; set; }
        public string SizeName { get; set; }

        public ICollection<ConnectTable> DetailsManager { get; set; }

        public ICollection<Quantities> DetailsClient { get; set; }
    }
}
