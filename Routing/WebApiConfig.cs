using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SCXM.CertificateValidator.Routing
{
    public class WebApiConfig 
    {
        public void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(this.RegisterRoutes);
        }

        protected void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("CertificateChecker", "api/tools/certificates/check/{id}", new
            {
                controller = "CertificateChecker",
                action = "CheckCertificates"
            });
        }
    }
}