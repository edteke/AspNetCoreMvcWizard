using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvcWizard.Checkout.Entities;

namespace AspNetCoreMvcWizard.Checkout.Repositories
{
    /// <summary>
    /// Sample Repository, stores in memory.
    /// </summary>
    public class CheckoutRepository : ICheckoutRepository
    {

        private Dictionary<string, Entities.Checkout> _checkouts;

        public CheckoutRepository()
        {
            _checkouts = new Dictionary<string, Entities.Checkout>();
        }

        public Entities.Checkout StartNew(string userId)
        {
            var checkout = new Entities.Checkout()
            {
                CheckoutId = Guid.NewGuid(),
                UserId = userId,
            };

            _checkouts.Add(GetKey(checkout), checkout);

            return checkout;
        }

        private string GetKey(Entities.Checkout checkout)
        {
            return checkout.CheckoutId.ToString() + checkout.UserId;
        }

        public Entities.Checkout FindByIdForUser(Guid checkoutId, string userId)
        {
            var key = checkoutId.ToString() + userId;

            if (!_checkouts.ContainsKey(key))
            {
                return null;
            }

            return _checkouts[key];
        }

        public void MoveNext(Entities.Checkout checkout)
        {
            checkout.CurrentStep++;
        }

        public void MovePrevious(Entities.Checkout checkout)
        {
            checkout.CurrentStep--;
        }
    }
}
