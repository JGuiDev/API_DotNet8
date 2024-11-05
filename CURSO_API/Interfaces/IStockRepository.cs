using CURSO_API.Dtos.Stock;
using CURSO_API.Helpers;
using CURSO_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CURSO_API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetIDAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
