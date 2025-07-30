using System.Xml;

namespace SCXM.CertificateValidator.Configurations
{
    public class CertificateConfigurationRepository
    {
        public ICertificateConfigurator GetCertificateConfiguration()
        {
            XmlNode xmlNode = Sitecore.Configuration.Factory.GetConfigNode("scxm/certificateChecker");

            return Sitecore.Configuration.Factory.CreateObject<ICertificateConfigurator>(xmlNode);
        }
    }
}