using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.DTOs;
using PaymentAPI.Services;


namespace PaymentAPI.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IPaymentService _service;
        public ReportsController(IPaymentService service) => _service = service;

        [HttpGet("payments")]
        public async Task<IActionResult> GetPaymentReport([FromQuery] Filter filter)
            => Ok(await _service.GetPaymentReportAsync(filter));

        [HttpGet("card-balances")]
        public async Task<IActionResult> GetCardReport([FromQuery] Filter filter)
            => Ok(await _service.GetCardBalanceReportAsync(filter));
    }
}
