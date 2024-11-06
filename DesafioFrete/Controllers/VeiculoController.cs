using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext;
using DesafioFrete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioFrete.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly FreteDatabaseContext _context;

        public VeiculoController(FreteDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/veiculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        // GET: api/veiculo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(long id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return veiculo;
        }

        // POST: api/veiculo
        [HttpPost]
        public async Task<ActionResult<Veiculo>> CreateVeiculo([FromBody] Veiculo veiculo)
        {
            if(await _context.Veiculos.AnyAsync(v => v.Renavam.Equals(veiculo.Renavam)))
            {
                throw new Exception("Veículo já cadastrado!");
            }
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.VeiculoId }, veiculo);
        }

        // PUT: api/veiculo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVeiculo(long id, [FromBody] Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId)
            {
                return BadRequest();
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
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

        // DELETE: api/veiculo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(long id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeiculoExists(long id)
        {
            return _context.Veiculos.Any(e => e.VeiculoId == id);
        }
    }
}