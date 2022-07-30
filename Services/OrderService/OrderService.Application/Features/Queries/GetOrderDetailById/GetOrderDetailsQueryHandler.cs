﻿using AutoMapper;
using MediatR;
using OrderService.Application.Features.Queries.ViewModels;
using OrderService.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Queries.GetOrderDetailById
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDetailViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            return _mapper.Map<OrderDetailViewModel>(order);
        }
    }
}
