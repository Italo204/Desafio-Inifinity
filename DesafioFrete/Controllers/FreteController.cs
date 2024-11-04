using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext;
using DesafioFrete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioFrete.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        private readonly FreteDatabaseContext _context;

        public FreteController(FreteDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/frete
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frete>>> GetFretes()
        {
            return await _context.Fretes.ToListAsync();
        }

        // GET: api/frete/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Frete>> GetFrete(int id)
        {
            var frete = await _context.Fretes.FindAsync(id);
            if (frete == null)
            {
                return NotFound();
            }
            return frete;
        }

        // POST: api/frete
        [HttpPost]
        public async Task<ActionResult<Frete>> CreateFrete([FromBody] Frete frete)
        {
            _context.Fretes.Add(frete);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFrete), new { id = frete.FreteId }, frete);
        }

        // PUT: api/frete/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFrete(int id, [FromBody] Frete frete)
        {
            if (id != frete.FreteId)
            {
                return BadRequest();
            }

            _context.Entry(frete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/frete/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrete(int id)
        {
            var frete = await _context.Fretes.FindAsync(id);
            if (frete == null)
            {
                return NotFound();
            }

            _context.Fretes.Remove(frete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FreteExists(int id)
        {
            return _context.Fretes.Any(e => e.FreteId == id);
        }
    }
}