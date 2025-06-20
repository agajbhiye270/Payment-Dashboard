using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.DTOs;
using PaymentAPI.Services;

namespace PaymentAPI.Controllers
{
        [ApiController]
        [Route("api/payment")]
        
        public class PaymentController : ControllerBase
        {
            private readonly IPaymentService _paymentService;

            public PaymentController(IPaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            // POST: api/payment/process
            [HttpPost("process")]
            public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    var (transactionId, refundCode) = await _paymentService.ProcessPaymentAsync(request);
                    return Ok(new
                    {
                        TransactionId = transactionId,
                        RefundCode = refundCode,
                        Message = "Payment processed successfully."
                    });
                }
                catch (System.Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

            // POST: api/payment/refund
            [HttpPost("refund")]
            public async Task<IActionResult> Refund([FromBody] RefundRequest request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _paymentService.RefundAsync(request);
                if (!result)
                    return BadRequest(new { message = "Refund failed. Invalid transaction or already confirmed." });

                return Ok(new { message = "Refund successful." });
            }
        }
    
}
