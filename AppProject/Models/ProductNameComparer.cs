using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Models
{
    public class StudentNameComparer : IEqualityComparer<Productes>
    {
        public bool Equals(Productes x, Productes y)
        {
            if (string.Equals(x.ProductName, y.ProductName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Productes obj)
        {
            return obj.ImgId.GetHashCode();
        }
    }
}
