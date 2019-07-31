﻿using Festival.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Festival.Servises
{
    public class BandService : IBandService
    {
        private readonly BandContext _context;

        public BandService(BandContext context)
        {
            _context = context;
        }

        public async Task<List<Band>> GetBands()
        {
            return await _context.Bands.ToListAsync();
        }

        public async Task<Band> GetBand(int id)
        {
            var band = await _context.Bands.FindAsync(id);

            return band;
        }

        public async Task<Band> PutBand(Band band)
        {
            _context.Entry(band).State = EntityState.Modified;

            var bandFromDB = await _context.Bands.FindAsync(band.Id);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return bandFromDB;
        }

        public async Task<Band> PostBand(Band band)
        {
            _context.Bands.Add(band);
            await _context.SaveChangesAsync();

            return band;
        }

        public async Task DeleteBand(int id)
        {
            var band = _context.Bands.Find(id);

            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();
        }

    }
}
