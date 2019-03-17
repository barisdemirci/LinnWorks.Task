using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;
using LinnWorks.Task.Repositories.Sales;
using LinnWorks.Task.Repositories.UnitOfWork;

namespace LinnWorks.Task.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async System.Threading.Tasks.Task AddAllAsync(IEnumerable<SaleDto> salesDto)
        {
            var sales = mapper.Map<IEnumerable<Sale>>(salesDto);
            await unitOfWork.Sales.AddAllAsync(sales);
        }

        public async Task<IEnumerable<SaleDto>> GetSalesAsync()
        {
            List<SaleDto> result = new List<SaleDto>();
            result.Add(new SaleDto()
            {
                Country = "TR"
            });

            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}