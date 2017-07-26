using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvcWizard.Checkout.Entities;
using Microsoft.Extensions.Primitives;

namespace AspNetCoreMvcWizard.Checkout.Repositories
{
    public interface ICheckoutRepository
    {
        Entities.Checkout StartNew(string userId);

        Entities.Checkout FindByIdForUser(Guid checkoutId, string userId);

        void MoveNext(Entities.Checkout checkout);
        void MovePrevious(Entities.Checkout checkout);
    }
}
