namespace Kwikemark.Entities.DTOs
{
    using System.ComponentModel.DataAnnotations;
    public class TypeProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
