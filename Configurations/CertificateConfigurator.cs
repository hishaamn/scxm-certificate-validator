using SCXM.CertificateValidator.Models;
using System.Collections.Generic;
using System.Xml;

namespace SCXM.CertificateValidator.Configurations
{
    public class CertificateConfigurator : ICertificateConfigurator
    {
        public List<CertificateConfigModel> CertificateConfigList = new List<CertificateConfigModel>();

        public IEnumerable<CertificateConfigModel> Certificates { get { return this.CertificateConfigList; } }

        public void AddCertificates(XmlNode node)
        {
            if (node != null)
            {
                this.CertificateConfigList.Add(new CertificateConfigModel
                {
                    Name = node.Attributes["name"]?.Value,
                    Thumbprint = node.Attributes["thumbprint"]?.Value
                });
            }
        }
    }
}