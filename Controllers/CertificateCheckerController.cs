using SCXM.CertificateValidator.Configurations;
using SCXM.CertificateValidator.Models;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace SCXM.CertificateValidator.Controllers
{
    public class CertificateCheckerController : ServicesApiController
    {
        [HttpGet]
        [Route("certificates/check/{certificateName?}")]
        public IHttpActionResult CheckCertificates(string certificateName = null)
        {
            try
            {
                var certs = new List<CertificateInfoModel>();

                foreach (StoreLocation location in Enum.GetValues(typeof(StoreLocation)))
                {
                    foreach (StoreName storeName in Enum.GetValues(typeof(StoreName)))
                    {
                        try
                        {
                            var store = new X509Store(storeName, location);
                            store.Open(OpenFlags.ReadOnly);

                            foreach (var cert in store.Certificates)
                            {
                                certs.Add(new CertificateInfoModel
                                {
                                    Subject = cert.Subject,
                                    Thumbprint = cert.Thumbprint,
                                    Issuer = cert.Issuer,
                                    FriendlyName = cert.FriendlyName,
                                    NotBefore = cert.NotBefore,
                                    NotAfter = cert.NotAfter,
                                    StoreName = storeName.ToString(),
                                    StoreLocation = location.ToString()
                                });
                            }

                            store.Close();
                        }
                        catch (Exception ex)
                        {
                            Sitecore.Diagnostics.Log.Error($"[CERTIFICATEVALIDATOR] :: Error occurred while reading the certificate store. Message: {ex.Message}. StackTrace: {ex.StackTrace}", this);
                        }
                    }
                }

                var rep = new CertificateConfigurationRepository();

                var config = rep.GetCertificateConfiguration();

                if (config != null)
                {
                    var certificates = config.Certificates;

                    var certificateResultList = new List<CertificateInfoModel>();

                    foreach (var certConfig in certificates)
                    {
                        var cert = certs.FirstOrDefault(c => c.Thumbprint.Equals(certConfig.Thumbprint, StringComparison.OrdinalIgnoreCase) || c.Subject.Contains(certConfig.Name) || c.FriendlyName.Equals(certConfig.Name, StringComparison.OrdinalIgnoreCase));

                        if (cert != null)
                        {
                            certificateResultList.Add(cert);
                        }
                    }

                    return this.Ok(certificateResultList);
                }

                return this.Ok(new { Result = "No certificate has been configured in the <certifcateChecker>" });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
