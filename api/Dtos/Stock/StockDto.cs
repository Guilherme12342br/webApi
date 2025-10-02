using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data.Dtos.Stock
{
    public class StockDto
    {
        public int Id {get; set;}

        public string Symbol {get;set;} = string.Empty;

        public string CompanyName {get;set;} = string.Empty;

        
    }
}