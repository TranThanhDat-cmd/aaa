using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int AccCustomerId { get; set; }
        public DateTime? CreateAtTime { get; set; }

        public virtual AccCustomer AccCustomer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
