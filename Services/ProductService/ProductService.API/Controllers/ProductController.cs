using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Command.Commands.CreateProduct;
using ProductService.Command.Commands.DeleteProduct;
using ProductService.Query.Queries.GetAllProducts;
using ProductService.Query.Queries.GetProductById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<CreateProductCommandResponse> CreateProductAsync(CreateProductCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<DeleteProductCommandResponse> DeleteProductAsync(DeleteProductCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllProductsQueryResponse>> GetAllProductsAsync(GetAllProductsQueryRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<GetByIdQueryResponse> GetProductByIdAsync(GetByIdQueryRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
