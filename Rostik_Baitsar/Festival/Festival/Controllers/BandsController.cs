﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Festival.Models;

namespace Festival.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly BandContext _context;

        public BandsController(BandContext context)
        {
            _context = context;
        }

        // GET: api/Bands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Band>>> GetBands()
        {
            return await _context.Bands.ToListAsync();
        }

        // GET: api/Bands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Band>> GetBand(int id)
        {
            var band = await _context.Bands.FindAsync(id);

            if (band == null)
            {
                return NotFound();
            }

            return band;
        }

        // PUT: api/Bands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBand(int id, Band band)
        {
            if (id != band.Id)
            {
                return BadRequest();
            }

            _context.Entry(band).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BandExists(id))
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

        // POST: api/Bands
        [HttpPost]
        public async Task<ActionResult<Band>> PostBand(Band band)
        {
            _context.Bands.Add(band);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBand", new { id = band.Id }, band);
        }

        // DELETE: api/Bands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Band>> DeleteBand(int id)
        {
            var band = await _context.Bands.FindAsync(id);
            if (band == null)
            {
                return NotFound();
            }

            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();

            return band;
        }

        private bool BandExists(int id)
        {
            return _context.Bands.Any(e => e.Id == id);
        }
    }
}
