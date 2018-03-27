using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ContextSpike
{
    [Route("/")]
    public class CharacterController : Controller
    {
        private readonly CharacterContext _context;

        public CharacterController(CharacterContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Add([FromBody] Character character)
        {
            await _context.AddAsync(character);
            _context.SaveChanges();
            return Ok();
        }
    }
}