using OrderService.Domain.Common;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class CardType : CustomEnumeration
    {
        private CardType(int id, string name) : base(id, name)
        {
        }

        public static readonly CardType MasterCard = new(1, nameof(MasterCard));
        public static readonly CardType Visa = new(2, nameof(Visa));
    }
}
