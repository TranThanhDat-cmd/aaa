using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class Categogy
    {
        public Categogy()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
