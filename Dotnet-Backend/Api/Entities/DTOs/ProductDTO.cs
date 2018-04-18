namespace Kwikemark.Entities.DTOs
{
    using System.ComponentModel.DataAnnotations;
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int IdTypeProduct { get; set; }
    }
}
