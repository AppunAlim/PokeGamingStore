using System;

namespace PokeGamingStore.Models
{
    public class History<T>
    {
        public string LogId { get; set; }
        public string UserId { get; set; } 
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public T Data { get; set; }
    }
}