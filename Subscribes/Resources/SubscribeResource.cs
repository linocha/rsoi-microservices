using System;

namespace Subscribes.Resources
{
    public class SubscribeResource
    {
        public int Id { get; set; }
        public int ExtUserId { get; set; }
        public int ExtProdId { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
    }
}