using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStock()
        {
            // veri tabanında UserName veya UserId üzerinden gerekli dataları çekecek.// it will pull the necessary data from the database via UserName or UserId.//

            var userName = HttpContext.User.Identity.Name;
            var UserIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return Ok($"Stock işlemleri =>UserName:{userName}- UserId:{UserIdClaim.Value}");
            
        }
    }
}
