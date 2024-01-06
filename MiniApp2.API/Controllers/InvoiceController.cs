using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetInvoices()
        {
            // veri tabanında UserName veya UserId üzerinden gerekli dataları çekecek.// it will pull the necessary data from the database via UserName or UserId.//

            var userName = HttpContext.User.Identity.Name;
            var UserIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return Ok($"Invoices İşlemleri =>UserName:{userName}- UserId:{UserIdClaim.Value}");

        }
    }
}
