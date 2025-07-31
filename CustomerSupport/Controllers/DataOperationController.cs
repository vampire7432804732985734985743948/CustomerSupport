using CustomerSupport.DataBaseConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Controllers
{
    [Route("api/client-requests")]
    public class DataOperationController : Controller
    {
        private readonly AppDbContext _context;

        public DataOperationController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientRequests()
        {
            var cases = await _context.ContactSupportRequests.ToListAsync();
            return Ok(cases);
        }
    }
}
