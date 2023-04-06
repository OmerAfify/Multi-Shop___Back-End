using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesssLogic.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Models.Filtration;
using Models.Interfaces.IBusinessRepository;
using Models.Models;
using Models.Pagination;
using OnlineShopWebAPIs.BusinessLogic.DBContext;

namespace BusinessLogic.Repository.BusinessRepository
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        private readonly OnlineShopDbContext _context;

        public ProductsRepository(OnlineShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pagination<Product>> GetProductsByFilteration(FilteringObject filterObject)
        {

            IQueryable<Product> query = _context.Tb_Products;

            //filter by category

            if (filterObject.categoryId != 0)
            {
                query = query.Where(p => p.categoryId == filterObject.categoryId);
            }


            // filter by sort 
            if (!string.IsNullOrEmpty( filterObject.sortBy))
            {
                switch (filterObject.sortBy)
                {
                    case "aToZ":
                        query = query.OrderBy(a => a.productName);   
                        break;
                    case "zToA":
                        query = query.OrderByDescending(a => a.productName);
                        break;
                    case "priceLtoH":
                        query = query.OrderBy(a => a.salesPrice);
                        break;
                    case "priceHtoL":
                        query = query.OrderByDescending(a => a.salesPrice);
                        break;
                    default:
                        break;
                }

              }


            var pagingData = new Pagination<Product>();

            pagingData.count = query.Count();


            if (filterObject.requestParameters != null)
                query = query.Skip((filterObject.requestParameters.PageNumber - 1) * filterObject.requestParameters.PageSize)
                             .Take(filterObject.requestParameters.PageSize);


            pagingData.pageNumber = (filterObject.requestParameters != null) ? filterObject.requestParameters.PageNumber : 1;
            pagingData.pageSize = (filterObject.requestParameters != null) ? filterObject.requestParameters.PageSize : pagingData.count;
            pagingData.data = await query.Include(p=>p.category).Include(p=>p.productImages).AsNoTracking().ToListAsync();


            return pagingData;




        }
    }



}
