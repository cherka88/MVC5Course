using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.Is�R�� == false);
        }

        public IQueryable<Product> All(bool showall)
        {
            if (showall)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }
        {

        }
        public Product Get�浧���ByID(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get�ӫ~��ƦC��(bool active, bool showall = true)
        {
            IQueryable<Product> all = this.All();
            if (showall)
            {
                all = base.All();
            }
            return all.Where(p => p.Active.HasValue && p.Active == active)
                .OrderByDescending(p => p.ProductId).Take(10);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}