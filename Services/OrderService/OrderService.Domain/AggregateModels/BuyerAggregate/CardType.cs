using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.BuyerAggregate
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
