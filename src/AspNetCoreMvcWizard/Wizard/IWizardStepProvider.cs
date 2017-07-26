using Microsoft.AspNetCore.Http;

namespace AspNetCoreMvcWizard.Wizard
{
    /// <summary>
    /// Determines the current step based on HTTP Context information
    /// </summary>
    public interface IWizardStepProvider
    {
        int GetCurrentStep(HttpContext httpContext);
    }
}