using AutoMapper;
using OrderService.Application.Features.Queries.ViewModels;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.MapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDetailViewModel, Order>().ReverseMap();
        }
    }
}
