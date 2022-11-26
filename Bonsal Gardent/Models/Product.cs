using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            CommentProducts = new HashSet<CommentProduct>();
            OrderDetails = new HashSet<OrderDetail>();
            Pictures = new HashSet<Picture>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public string? Place { get; set; }
        public string Address { get; set; } = null!;
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public string? Price { get; set; }
        public byte? Amount { get; set; }

        public virtual Categogy Category { get; set; } = null!;
        public virtual Type Type { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<CommentProduct> CommentProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Picture>? Pictures { get; set; }
    }
}
