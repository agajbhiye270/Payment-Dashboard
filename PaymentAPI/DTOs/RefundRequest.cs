﻿namespace PaymentAPI.DTOs
{
    public class RefundRequest
    {
        public string TransactionId { get; set; }
        public string RefundCode { get; set; }
    }
}
