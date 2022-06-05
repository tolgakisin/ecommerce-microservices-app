﻿using AutoMapper;
using BasketService.API.Contracts.Models;
using BasketService.Data.Contracts.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Mappings.Basket
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<CustomerBasketModel, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemModel, BasketItem>().ReverseMap();
        }
    }
}
