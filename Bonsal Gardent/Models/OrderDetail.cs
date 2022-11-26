using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int CustomerOrderId { get; set; }
        public int? Amount { get; set; }
        public int? ProductId { get; set; }
        public string? Note { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; } = null!;
        public virtual Product? Product { get; set; }
    }
}
