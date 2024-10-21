using ICMA_LEARN.API.Data.Entity;
using ICMA_LEARN.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICMA_LEARN.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : ControllerBase
    {
        private readonly ICMAContext _context;

        public AttemptController(ICMAContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attempt>>> GetAttempts()
        {
            return await _context.Attempts.Include(a => a.Course).Include(a => a.User).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attempt>> GetAttempt(int id)
        {
            var attempt = await _context.Attempts.Include(a => a.Course).Include(a => a.User).FirstOrDefaultAsync(a => a.AttemptID == id);
            if (attempt == null)
            {
                return NotFound();
            }
            return attempt;
        }
        [HttpPost]
        public async Task<ActionResult<Attempt>> CreateAttempt(Attempt attempt)
        {
            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAttempt), new { id = attempt.AttemptID }, attempt);
        }

        [HttpGet("TopUsersByCategory")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopUsersByCategory()
        {
            var topUsers = await _context.Attempts
                .Where(a => a.CompletionStatus == "Complete")
                .GroupBy(a => a.Course.CategoryID)
                .Select(g => new
                {
                    CategoryName = g.First().Course.Category.CategoryName,
                    TopUser = g.OrderByDescending(a => a.Score)
                               .Select(a => new { a.User.UserName, a.Score })
                               .FirstOrDefault()
                })
                .ToListAsync();

            return topUsers;
        }
    }

}
