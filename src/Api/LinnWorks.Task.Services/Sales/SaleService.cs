using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LinnWorks.Task.Dtos;
using LinnWorks.Task.Dtos.Sales;
using LinnWorks.Task.Entities;
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
            if (salesDto == null) throw new ArgumentNullException(nameof(salesDto));

            var sales = mapper.Map<IEnumerable<Sale>>(salesDto);
            await unitOfWork.Sales.AddAllAsync(sales);
        }

        public IEnumerable<SaleDto> GetFilteredSales(GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            var sales = unitOfWork.Sales.GetFilteredSales(requestDto);
            return mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        public FilterParametersDto GetFilterParameters(GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            var parameters = unitOfWork.Sales.GetFilterParameters(requestDto);
            return mapper.Map<FilterParametersDto>(parameters);
        }

        public int GetLastPageIndex(GetSalesRequestDto requestDto)
        {
            if (requestDto == null) throw new ArgumentNullException(nameof(requestDto));

            return unitOfWork.Sales.GetLastPageIndex(requestDto);
        }

        public void UpdateSales(IEnumerable<SaleDto> salesDto)
        {
            if (salesDto == null) throw new ArgumentNullException(nameof(salesDto));

            var sales = mapper.Map<IEnumerable<Sale>>(salesDto);
            unitOfWork.Sales.UpdateRange(sales);
        }
    }
}