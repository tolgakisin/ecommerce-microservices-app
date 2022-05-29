using MediatR;
using ProductService.Command.CommandModels;

namespace ProductService.Command.Commands.CreateProduct
{
    public class CreateProductCommandRequest : CreateProductModel, IRequest<CreateProductCommandResponse>
    {
        public CreateProductCommandRequest()
        {

        }

        public CreateProductCommandRequest(string name, int quantity, decimal price, string description)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = description;
        }
    }
}
