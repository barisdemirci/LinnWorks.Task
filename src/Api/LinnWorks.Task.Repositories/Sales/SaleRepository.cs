using LinnWorks.Task.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorks.Task.Repositories.Sales
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return Context as ApplicationDbContext;
            }
        }

        public SaleRepository(DbContext context) : base(context)
        {

        }
    }
}