namespace PaymentAPI.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string RefundCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }   // Added for scheduling job
        public string Status { get; set; }        // "Held", "Confirmed", "Refunded", etc.
        public Guid CardId { get; set; }
        public Card Card { get; set; }
    }
}
