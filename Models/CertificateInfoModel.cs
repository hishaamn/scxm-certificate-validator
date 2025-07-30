using System;

namespace SCXM.CertificateValidator.Models
{
    public class CertificateInfoModel
    {
        public string Subject { get; set; }

        public string Thumbprint { get; set; }
        
        public string Issuer { get; set; }
        
        public string FriendlyName { get; set; }
        
        public DateTime NotBefore { get; set; }
        
        public DateTime NotAfter { get; set; }
        
        public bool IsExpired => DateTime.Now > NotAfter;
        
        public string StoreName { get; set; }
        
        public string StoreLocation { get; set; }
    }
}