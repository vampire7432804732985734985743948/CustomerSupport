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
            return View("~/Views/Home/Index.cshtml", cases);
        }
        [HttpPost]
        public async Task<IActionResult> Reply(int id)
        {
            var caseItem = await _context.ContactSupportRequests.FindAsync(id);
            if (caseItem == null) return NotFound();

            return View(caseItem);
        }
        public IActionResult Privacy()
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
