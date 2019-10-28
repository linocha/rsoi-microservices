using System;

namespace Subscribes.Domain.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        public int ExtUserId { get; set; }
        public int ExtProdId { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
    }
}