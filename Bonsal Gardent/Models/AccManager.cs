using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class AccManager
    {
        public AccManager()
        {
            CommentProducts = new HashSet<CommentProduct>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; } = null!;
        public int? Type { get; set; }

        public virtual ICollection<CommentProduct> CommentProducts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
