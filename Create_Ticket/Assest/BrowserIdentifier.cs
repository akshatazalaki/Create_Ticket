using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Create_Ticket.Assest
{
    public class BrowserIdentifier
    {
        public static string GenerateBrowserFingerprint(HttpRequest request)
        {
            // Gather browser details
            string userAgent = request.UserAgent;
            string userIpAddress = request.UserHostAddress;
            string acceptLanguage = request.Headers["Accept-Language"];
            string platform = request.Browser.Platform;
            string cookiesEnabled = request.Browser.Cookies ? "1" : "0";

            // Combine these details to form a fingerprint
            string fingerprint = $"{userAgent}_{userIpAddress}_{acceptLanguage}_{platform}_{cookiesEnabled}";

            // Create a hash of the fingerprint
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] fingerprintBytes = Encoding.UTF8.GetBytes(fingerprint);
                byte[] hashBytes = sha256.ComputeHash(fingerprintBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}