using PaymentAPI.Data;

namespace PaymentAPI.Job
{
    public class PaymentConfirmationJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentConfirmationJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var nextRun = now.Date.AddDays(1);
                var delay = nextRun - now;
                await Task.Delay(delay, stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<PaymentAppDbContext>();

                var heldTransactions = context.Payments.Where(t => t.Status == "Held" && t.CreatedAt.Date < DateTime.UtcNow.Date);
                foreach (var trx in heldTransactions)
                {
                    trx.Status = "Confirmed";
                }
                await context.SaveChangesAsync();
            }
        }
    }
    
}
