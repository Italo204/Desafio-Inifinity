using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFrete.Models;
using DesafioFrete.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFrete.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Aplica autorização a todas as rotas deste controller
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("roles")]
        public ActionResult<IEnumerable<string>> GetRoles()
        {
            var roles = _roleService.Roles.Keys.ToList();
            return Ok(roles);
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult CreateRole([FromBody] Role role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name))
            {
                return BadRequest("Role is null or has an empty name.");
            }

            _roleService.AddRole(role);
            return CreatedAtAction(nameof(GetRoles), new { name = role.Name }, role);
        }

        // PUT: api/roles/{name}
        [HttpPut("{name}")]
        public ActionResult UpdateRole(string name, [FromBody] Role updatedRole)
        {
            if (updatedRole == null || string.IsNullOrWhiteSpace(updatedRole.Name))
            {
                return BadRequest("Updated role is null or has an empty name.");
            }

            if (!_roleService.Roles.ContainsKey(name))
            {
                return NotFound($"Role with name '{name}' not found.");
            }

            // Update the role
            _roleService.Roles[name] = updatedRole;
            _roleService.SaveRoles();
            return NoContent();
        }

        // DELETE: api/roles/{name}
        [HttpDelete("{name}")]
        public ActionResult DeleteRole(string name)
        {
            if (!_roleService.Roles.ContainsKey(name))
            {
                return NotFound($"Role with name '{name}' not found.");
            }

            _roleService.Roles.Remove(name);
            _roleService.SaveRoles();
            return NoContent();
        }
    }
}