using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class CommentProduct
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int? AccManagerId { get; set; }
        public string Answer { get; set; } = null!;
        public int? ProductId { get; set; }
        public int AccCustomerId { get; set; }

        public virtual AccCustomer AccCustomer { get; set; } = null!;
        public virtual AccManager? AccManager { get; set; }
        public virtual Product? Product { get; set; }
    }
}
