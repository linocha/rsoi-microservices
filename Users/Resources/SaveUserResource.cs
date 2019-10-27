using System.ComponentModel.DataAnnotations;

namespace Users.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public double Cost { get; set; }
    }
}