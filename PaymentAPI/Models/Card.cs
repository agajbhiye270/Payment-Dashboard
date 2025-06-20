using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentAPI.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsValid { get; set; }

       public string? CVV { get; set; }
    }
}
