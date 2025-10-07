using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context) {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(bool status)
        {
            if (status)
            {
                return await _context.Stock.Where(s => s.Status == "ATIVO").ToListAsync();
            }
            else
            {
                return await _context.Stock.Where(s => s.Status == "DESATIVADO").ToListAsync();
            }
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var selectedStockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(selectedStockModel == null)
            {
                return null;
            }      
            selectedStockModel.Status = "DESATIVADO";
            await _context.SaveChangesAsync();
            return selectedStockModel;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock?> AtivarAsync(int id)
        {
            var selectedStockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(selectedStockModel == null)
            {
                return null;
            }

            selectedStockModel.Status = "ATIVO";
            await _context.SaveChangesAsync();

            return selectedStockModel;
        }
    }
}
