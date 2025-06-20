using PaymentAPI.DTOs;
using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task<string> ValidateCardAsync(string cardNumber);

        Task<(string transactionId, string refundCode)> ProcessPaymentAsync(PaymentRequest request);

        Task<bool> ConfirmPaymentsAsync();

        Task<bool> RefundAsync(RefundRequest request);

        Task<PaginatedResult<Payment>> GetPaymentReportAsync(Filter filter);

        Task<PaginatedResult<Card>> GetCardBalanceReportAsync(Filter filter);
    }
}
