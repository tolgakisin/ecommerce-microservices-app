﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Base
{
    public abstract class BaseModel : IModel
    {
        public Guid Id { get; set; }
    }
}
