using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SCXM.CertificateValidator.Controllers;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SCXM.CertificateValidator.Routing
{
    public class ControllerRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            try
            {
                serviceCollection.Replace(ServiceDescriptor.Transient(typeof(CertificateCheckerController), typeof(CertificateCheckerController)));
            }
            catch (ReflectionTypeLoadException ex)
            {
                var messages = string.Join(Environment.NewLine, ex.LoaderExceptions.Select(e => e.Message));
                throw new Exception("Failed to load controller due to: " + messages);
            }
        }
    }
}