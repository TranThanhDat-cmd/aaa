using System;
using System.Collections.Generic;

namespace Bonsal_Gardent.Models
{
    public partial class Picture
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Path { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
