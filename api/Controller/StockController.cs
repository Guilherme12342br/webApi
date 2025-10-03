using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using api.Data;
using api.Models;
using api.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {


        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stock = _context.Stock.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) {
            var stock = _context.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequiredDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); 
        }
    }
}