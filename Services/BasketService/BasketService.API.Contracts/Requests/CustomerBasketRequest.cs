﻿using BasketService.API.Contracts.Base;
using BasketService.API.Contracts.Models;
using BasketService.Data.Contracts.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Requests
{
    public class CustomerBasketRequest : BaseRequest<CustomerBasketModel, CustomerBasket>
    {
    }
}
