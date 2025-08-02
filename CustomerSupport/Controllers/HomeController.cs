using CustomerSupport.DataBaseConnection;
using CustomerSupport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _context = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cases = await _context.ContactSupportRequests.ToListAsync();
            return View(cases);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Reply(int id)
        {
            var singleCase = await _context.ContactSupportRequests.FindAsync(id);

            if (singleCase == null)
            {
                return NotFound();
            }

            return View("~/Views/ContactSupportReplyForm/Reply.cshtml", singleCase);
        }

        [HttpPost]
        public IActionResult SendReply(ContactSupportReplyModel reply) 
        {
            if (reply == null) 
            {
                return RedirectToAction("Reply");
            }

            return View();
        }
        public IActionResult ArchiveRequestsCases()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
