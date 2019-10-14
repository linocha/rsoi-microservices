using System.ComponentModel.DataAnnotations;

namespace Products.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public double Cost { get; set; }
    }
}