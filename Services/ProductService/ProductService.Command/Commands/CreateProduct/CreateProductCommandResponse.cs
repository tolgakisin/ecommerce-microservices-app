using System;

namespace ProductService.Command.Commands.CreateProduct
{
    public class CreateProductCommandResponse
    {
        public CreateProductCommandResponse()
        {

        }

        public CreateProductCommandResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
