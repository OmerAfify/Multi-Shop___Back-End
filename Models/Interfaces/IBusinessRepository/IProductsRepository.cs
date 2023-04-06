
using System.Threading.Tasks;
using Models.Filtration;
using Models.Interfaces.IGenericRepository;
using Models.Models;
using Models.Pagination;

namespace Models.Interfaces.IBusinessRepository
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        public Task<Pagination<Product>> GetProductsByFilteration(FilteringObject filterObject);
    }
}
