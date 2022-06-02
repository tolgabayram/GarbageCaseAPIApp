using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarbageCaseAPI.Data;
using GarbageCaseAPI.Models;
using GarbageCaseAPI.Authentication.Entity;
using Microsoft.AspNetCore.Authorization;
using GarbageCaseAPI.Authentication.Interface;

namespace GarbageCaseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WasteTypesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJwtAuth jwtAuth;

        public WasteTypesController(DataContext context, IJwtAuth jwtAuth)
        {
            _context = context;
            this.jwtAuth = jwtAuth;
        }

        // GET: api/WasteTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasteType>>> GetWasteTypes()
        {
          if (_context.WasteTypes == null)
          {
              return NotFound();
          }
            return await _context.WasteTypes.ToListAsync();
        }

        // GET: api/WasteTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WasteType>> GetWasteType(int id)
        {
          if (_context.WasteTypes == null)
          {
              return NotFound();
          }
            var wasteType = await _context.WasteTypes.FindAsync(id);

            if (wasteType == null)
            {
                return NotFound();
            }

            return wasteType;
        }

        // PUT: api/WasteTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasteType(int id, WasteType wasteType)
        {
            if (id != wasteType.Id)
            {
                return BadRequest();
            }

            _context.Entry(wasteType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WasteTypeExists(id))
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

        // POST: api/WasteTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WasteType>> PostWasteType(WasteType wasteType)
        {
          if (_context.WasteTypes == null)
          {
              return Problem("Entity set 'DataContext.WasteTypes'  is null.");
          }
            _context.WasteTypes.Add(wasteType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWasteType", new { id = wasteType.Id }, wasteType);
        }

        // DELETE: api/WasteTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteType(int id)
        {
            if (_context.WasteTypes == null)
            {
                return NotFound();
            }
            var wasteType = await _context.WasteTypes.FindAsync(id);
            if (wasteType == null)
            {
                return NotFound();
            }

            _context.WasteTypes.Remove(wasteType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WasteTypeExists(int id)
        {
            return (_context.WasteTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [AllowAnonymous]
        // POST api/<WasteTypeController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
