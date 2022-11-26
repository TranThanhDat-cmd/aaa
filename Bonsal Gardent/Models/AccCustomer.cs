using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class AccCustomer
    {
        public AccCustomer()
        {
            Carts = new HashSet<Cart>();
            CommentProducts = new HashSet<CommentProduct>();
            CustomerOrders = new HashSet<CustomerOrder>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; } = null!;

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<CommentProduct> CommentProducts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
