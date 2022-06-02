using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarbageCaseAPI.Data;
using GarbageCaseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using GarbageCaseAPI.Authentication.Interface;
using GarbageCaseAPI.Authentication.Entity;

namespace GarbageCaseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WasteRecordsController : ControllerBase
    {
      
        private readonly DataContext _context;
        private readonly IJwtAuth jwtAuth;
        public WasteRecordsController(DataContext context, IJwtAuth jwtAuth)
        {
            _context = context;
            this.jwtAuth = jwtAuth;
        }

        // GET: api/WasteRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasteRecord>>> GetWasteRecords()
        {
          if (_context.WasteRecords == null)
          {
              return NotFound();
          }
            return await _context.WasteRecords.ToListAsync();
        }

        // GET: api/WasteRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WasteRecord>> GetWasteRecord(int id)
        {
          if (_context.WasteRecords == null)
          {
              return NotFound();
          }
            var wasteRecord = await _context.WasteRecords.FindAsync(id);

            if (wasteRecord == null)
            {
                return NotFound();
            }

            return wasteRecord;
        }

        // PUT: api/WasteRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasteRecord(int id, WasteRecord wasteRecord)
        {
            if (id != wasteRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(wasteRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WasteRecordExists(id))
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

        // POST: api/WasteRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WasteRecord>> PostWasteRecord(WasteRecord wasteRecord)
        {
          if (_context.WasteRecords == null)
          {
              return Problem("Entity set 'DataContext.WasteRecords'  is null.");
          }
            _context.WasteRecords.Add(wasteRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWasteRecord", new { id = wasteRecord.Id }, wasteRecord);
        }

        // DELETE: api/WasteRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteRecord(int id)
        {
            if (_context.WasteRecords == null)
            {
                return NotFound();
            }
            var wasteRecord = await _context.WasteRecords.FindAsync(id);
            if (wasteRecord == null)
            {
                return NotFound();
            }

            _context.WasteRecords.Remove(wasteRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WasteRecordExists(int id)
        {
            return (_context.WasteRecords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [AllowAnonymous]
        // POST api/<WasteRecordController>
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
