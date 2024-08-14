using EditorText.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EditorText.Controllers
{
    [Authorize] 
    public class DocsController : Controller
    {
        

        private readonly ApplicationDbContext _context;
        public DocsController(ApplicationDbContext context)
        {

        _context = context; }
    
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = from c in _context.Docs select c;
            applicationDbContext = applicationDbContext.Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            return  View (await applicationDbContext.Include(a=> a.User).ToListAsync());
        }
    }
}
