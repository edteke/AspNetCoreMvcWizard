using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreMvcWizard.Wizard;
using AspNetCoreMvcWizard.Checkout.Repositories;

namespace AspNetCoreMvcWizard.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutController(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public IActionResult Index()
        {
            // load data to show step 0
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            var checkout = _checkoutRepository.StartNew(User.Identity.Name);

            return RedirectToAction("Checkout", new { checkoutId = checkout.CheckoutId });
        }

            [WizardStep(0)]
            [ActionName("Checkout")]
            public IActionResult CheckoutStep0Get(Guid checkoutId)
            {
                var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

                if (checkout == null)
                {
                    return NotFound();
                }

                return View("Checkout0");
            }

            [WizardStep(0)]
            [ActionName("Checkout")]
            [HttpPost, ValidateAntiForgeryToken]
            public IActionResult CheckoutStep0Post(Guid checkoutId, string go)
            {
                // potential check here, return view if failed, 
                if (!ModelState.IsValid)
                {
                    return View("Checkout0");
                }

                var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

                if (checkout == null)
                {
                    return NotFound();
                }

                if (go == "Next")
                {
                    _checkoutRepository.MoveNext(checkout);
                }
                else
                {
                    _checkoutRepository.MovePrevious(checkout);
                }

                return RedirectToAction("Checkout", new { checkoutId = checkoutId});
            }

        [WizardStep(1)]
        [ActionName("Checkout")]
        public IActionResult CheckoutStep1Get(Guid checkoutId)
        {
            var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

            if (checkout == null)
            {
                return NotFound();
            }

            return View("Checkout1");
        }

        [WizardStep(1)]
        [ActionName("Checkout")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckoutStep1Post(Guid checkoutId, string go)
        {
            // potential check here, return view if failed, 
            if (!ModelState.IsValid)
            {
                return View("Checkout1");
            }

            var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

            if (checkout == null)
            {
                return NotFound();
            }

            if (go == "Next")
            {
                _checkoutRepository.MoveNext(checkout);
            }
            else
            {
                _checkoutRepository.MovePrevious(checkout);
            }

            return RedirectToAction("Checkout", new { checkoutId = checkoutId });

        }

        [WizardStep(2)]
        [ActionName("Checkout")]
        public IActionResult CheckoutStep2Get(Guid checkoutId)
        {
            var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

            if (checkout == null)
            {
                return NotFound();
            }

            return View("Checkout2");
        }

        [WizardStep(2)]
        [ActionName("Checkout")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckoutStep2Post(Guid checkoutId, string go)
        {
            // potential check here, return view if failed, 
            if (!ModelState.IsValid)
            {
                return View("Checkout2");
            }

            var checkout = _checkoutRepository.FindByIdForUser(checkoutId, User.Identity.Name);

            if (checkout == null)
            {
                return NotFound();
            }

            if (go == "Finish")
            {
                _checkoutRepository.MoveNext(checkout);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _checkoutRepository.MovePrevious(checkout);
                return RedirectToAction("Checkout", new { checkoutId = checkoutId });
            }
        }
    }
}