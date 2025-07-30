using SCXM.CertificateValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCXM.CertificateValidator.Configurations
{
    public interface ICertificateConfigurator
    {
        IEnumerable<CertificateConfigModel> Certificates { get; }
    }
}