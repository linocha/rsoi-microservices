using System;
using System.ComponentModel.DataAnnotations;

namespace Subscribes.Resources
{
    public class SaveSubscribeResource
    {

        [Required]
        public int ExtUserId { get; set; }
        
        [Required]
        public int ExtProdId { get; set; }
        
        public DateTime DataStart { get; set; }
        
        public DateTime DataEnd { get; set; }

    }
}