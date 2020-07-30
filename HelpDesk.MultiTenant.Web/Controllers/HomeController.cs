using HelpDesk.MultiTenant.Data;
using HelpDesk.MultiTenant.Models;
using HelpDesk.MultiTenant.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpDesk.MultiTenant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly QuodyMutiTenantDbContext _quodyMutiTenantDbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, QuodyMutiTenantDbContext quodyMutiTenantDbContext, ClaimsPrincipal claimsPrincipal, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _db = dbContext;
            _quodyMutiTenantDbContext = quodyMutiTenantDbContext;
            _claimsPrincipal = claimsPrincipal;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            ViewBag.Data = _db.ToDoItems.ToList();
            
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateTenant([FromForm] string name, [FromForm] string identifier)
        {
            var userId = _claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenant = await _quodyMutiTenantDbContext.QuodyTenantInfo.FirstOrDefaultAsync(f => f.UserId == userId);

            if (tenant == null) return NotFound();

            var tenantType = tenant.GetType();
            tenantType.GetProperty("Name").SetValue(tenant, name, null);
            tenantType.GetProperty("Identifier").SetValue(tenant, identifier, null);

            _quodyMutiTenantDbContext.Update(tenant);
            await _quodyMutiTenantDbContext.SaveChangesAsync();

            return RedirectPermanent($"{Request.Scheme}://{tenant.Identifier}.localhost:44343");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
