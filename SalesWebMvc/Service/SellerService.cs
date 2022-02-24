﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Service.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesWebMvc.Service
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsyc(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException error)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales.");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Not Found Id.");
            }

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }

        }
    }
}
