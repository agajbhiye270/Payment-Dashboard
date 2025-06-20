namespace PaymentAPI.Models
{
    public class RefundRequest
    {
        public string TransactionId { get; set; }
        public string RefundCode { get; set; }
    }
}
