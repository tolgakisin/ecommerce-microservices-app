
namespace ProductService.Command.Commands.DeleteProduct
{
    public class DeleteProductCommandResponse
    {
        public DeleteProductCommandResponse()
        {

        }
        public DeleteProductCommandResponse(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
