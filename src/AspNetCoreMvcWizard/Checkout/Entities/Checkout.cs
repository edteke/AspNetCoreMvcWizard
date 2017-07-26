using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvcWizard.Checkout.Entities
{
    public class Checkout
    {
        public Guid CheckoutId { get; set; }

        public string UserId { get; set; }

        public int CurrentStep { get; set; }

        // tentative properties: ShoppingCartId, or ProductId
    }
}
