namespace Bonsal_Gardent.Model
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public string? Place { get; set; }
        public string Address { get; set; } = null!;
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public string? Price { get; set; }
        public byte? Amount { get; set; }

        public List<IFormFile>? Images { get; set; }

    }
}
