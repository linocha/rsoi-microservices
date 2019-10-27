using System.ComponentModel.DataAnnotations;

namespace Users.Resources
{
    public class SaveUserResource
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}