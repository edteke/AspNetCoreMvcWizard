using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AspNetCoreMvcWizard.Wizard;
using AspNetCoreMvcWizard.Checkout.Repositories;

namespace AspNetCoreMvcWizard.Checkout
{
    public class CheckoutStepProvider : IWizardStepProvider
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutStepProvider(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public int GetCurrentStep(HttpContext httpContext)
        {
            string username = httpContext.User.Identity.Name;
            string rawCheckoutId = httpContext.Request.Query["checkoutId"];

            Guid checkoutId;

            if (!Guid.TryParse(rawCheckoutId, out checkoutId))
            {
                return -1;
            }

            var checkout = _checkoutRepository.FindByIdForUser(checkoutId, username);

            if (checkout == null)
            {
                return -1;
            }

            return checkout.CurrentStep;
        }
    }
}
