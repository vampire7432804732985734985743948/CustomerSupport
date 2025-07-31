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
        public async Task<IActionResult> Index()
        {
            var cases = await _context.ContactSupportRequests.ToListAsync();
            return View(cases);
        }
        [HttpPost]
        public async Task<IActionResult> Reply(int id)
        {
            var caseItem = await _context.ContactSupportRequests.FindAsync(id);
            if (caseItem == null) return NotFound();

            return View("~/Views/ContactSupportReplyForm/Reply.cshtml", caseItem);
        }
    }
}
