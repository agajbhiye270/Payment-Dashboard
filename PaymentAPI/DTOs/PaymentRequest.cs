namespace PaymentAPI.DTOs
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
