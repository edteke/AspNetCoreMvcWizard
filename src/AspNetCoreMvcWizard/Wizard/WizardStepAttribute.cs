using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using AspNetCoreMvcWizard.Checkout.Repositories;
using System.Reflection;

namespace AspNetCoreMvcWizard.Wizard
{
    /// <summary>
    /// Determines if an action is enabled or not to handle the current request 
    /// based on a checkout step provider.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WizardStepAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Indicates the step 
        /// </summary>
        public int Step { get; private set; }

        /// <summary>
        /// A provider to get steps based on current request data
        /// </summary>
        public Type WizardStepProviderType { get; set; }

        public WizardStepAttribute(int step)
        {
            Step = step;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            IWizardStepProvider stepsProvider;

            if (WizardStepProviderType != null 
                && 
                typeof(IWizardStepProvider).IsAssignableFrom(WizardStepProviderType))
            {
                stepsProvider = routeContext.HttpContext.RequestServices
                    .GetService(WizardStepProviderType) as IWizardStepProvider;
            }
            else
            {
                stepsProvider = routeContext.HttpContext.RequestServices
                    .GetService(typeof(IWizardStepProvider)) as IWizardStepProvider;
            }

            if (stepsProvider == null)
            {
                throw new Exception($"Can't create an instance of type '{WizardStepProviderType}'");
            }

            int currentStep = stepsProvider.GetCurrentStep(routeContext.HttpContext);

            return Step == currentStep;
        }
    }
}
