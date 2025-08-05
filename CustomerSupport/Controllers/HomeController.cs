using CustomerSupport.Constants.FhilterData;
using CustomerSupport.DataBaseConnection;
using CustomerSupport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;

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

        public async Task<IActionResult> Index(string category, string requestStatus, string submittionDate)
        {
            var query = _context.ContactSupportRequests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != Category.Any)
            {
                query = query.Where(x => x.Category == category);
            }

            if (!string.IsNullOrWhiteSpace(submittionDate) && submittionDate != Category.Any)
            {
                query = submittionDate switch
                {
                    SubmittionDate.Recent => query.OrderByDescending(r => r.CaseSubmitionTime),
                    SubmittionDate.Oldest => query.OrderBy(r => r.CaseSubmitionTime),
                    SubmittionDate.TheMostPopular => ApplyPopularitySort(query),
                    _ => query.OrderByDescending(r => r.CaseSubmitionTime)
                };
            }
             
            if (!string.IsNullOrWhiteSpace(requestStatus) && requestStatus != RequestStatus.Any)
            {
                query = query.Where(x => x.RequestStatus == requestStatus);
            }

            var selectedCases = await query.ToListAsync();
            return View(selectedCases);
        }
        private static IQueryable<ContactSupportRequestModel> ApplyPopularitySort(IQueryable<ContactSupportRequestModel> query)
        {
            return query.GroupBy(x => x.Category)
                        .OrderByDescending(o => o.Count())
                        .SelectMany(s => s);
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
