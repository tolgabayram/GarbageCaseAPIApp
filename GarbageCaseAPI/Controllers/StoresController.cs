using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarbageCaseAPI.Data;
using GarbageCaseAPI.Models;
using GarbageCaseAPI.Authentication.Interface;
using Microsoft.AspNetCore.Authorization;
using GarbageCaseAPI.Authentication.Entity;

namespace GarbageCaseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJwtAuth jwtAuth;
        public StoresController(DataContext context, IJwtAuth jwtAuth)
        {
            _context = context;
            this.jwtAuth = jwtAuth;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
          if (_context.Stores == null)
          {
              return Problem("Entity set 'DataContext.Stores'  is null.");
          }
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [AllowAnonymous]
        // POST api/<StoreController>
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
