using Bonsal_Gardent.Models;

namespace Bonsal_Gardent.Model
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int AccCustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? CreateAtTime { get; set; }
        public long TotalAmount { get; set; }
        public long TotalMoney { get; set; }
        public ICollection<OrderDetail> Items { get; set;}
    }
}
