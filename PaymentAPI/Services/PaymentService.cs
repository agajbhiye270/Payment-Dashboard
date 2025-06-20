using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.DTOs;
using PaymentAPI.Models;


namespace PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentAppDbContext _context;

        public PaymentService(PaymentAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> ValidateCardAsync(string cardNumber)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
            return card?.IsValid == true ? "Valid" : "Invalid";
        }

        public async Task<(string transactionId, string refundCode)> ProcessPaymentAsync(PaymentRequest request)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == request.CardNumber);
            if (card == null || !card.IsValid || card.Balance < request.Amount)
                throw new Exception("Invalid Card or insufficient balance");

            card.Balance -= request.Amount;

            var transactionId = Guid.NewGuid().ToString("N").Substring(0, 20);
            var refundCode = new Random().Next(1000, 9999).ToString();
            var now = DateTime.UtcNow;

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                TransactionId = transactionId,
                RefundCode = refundCode,
                Amount = request.Amount,
                Timestamp = now,
                CreatedAt = now,
                Status = "Held",
                IsConfirmed = false,
                Card = card
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return (transactionId, refundCode);
        }

        public async Task<bool> ConfirmPaymentsAsync()
        {
            var toConfirm = await _context.Payments
                .Where(p => p.Status == "Held" && p.CreatedAt.Date < DateTime.UtcNow.Date)
                .ToListAsync();

            toConfirm.ForEach(p =>
            {
                p.Status = "Confirmed";
                p.IsConfirmed = true;
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RefundAsync(RefundRequest request)
        {
            var payment = await _context.Payments.Include(p => p.Card)
                .FirstOrDefaultAsync(p => p.TransactionId == request.TransactionId && p.RefundCode == request.RefundCode);

            if (payment == null || payment.IsConfirmed)
                return false;

            payment.Card.Balance += payment.Amount;
            payment.Status = "Refunded";

            _context.Payments.Remove(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResult<Payment>> GetPaymentReportAsync(Filter filter)
        {
            var query = _context.Payments.Include(p => p.Card).AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PaginatedResult<Payment>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<PaginatedResult<Card>> GetCardBalanceReportAsync(Filter filter)
        {
            var query = _context.Cards.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PaginatedResult<Card>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
