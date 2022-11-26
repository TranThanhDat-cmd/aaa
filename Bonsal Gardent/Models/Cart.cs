using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Amount { get; set; }
        public int AccCustomerId { get; set; }

        public virtual AccCustomer AccCustomer { get; set; } = null!;
        public virtual Product? Product { get; set; }
    }
}
