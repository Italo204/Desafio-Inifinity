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
    public class UsuarioController : ControllerBase
    {
        private readonly FreteDatabaseContext _context;

        public UsuarioController(FreteDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            if(await _context.Usuarios.AnyAsync(u => u.Email.Equals(usuario.Email)))
            {
                throw new Exception("Já existe uma conta com o email fornecido!");
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioId }, usuario);
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(long id, [FromBody] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}