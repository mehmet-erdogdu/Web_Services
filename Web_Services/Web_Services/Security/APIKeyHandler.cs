using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Web_Services.Security
{
    public class APIKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var queryString = request.RequestUri.ParseQueryString();
            string apiKey = queryString["apiKey"];
            if (apiKey == User.Id)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(User.KullaniciAdi, "APIKey"));
                HttpContext.Current.User = principal;
            }
            return base.SendAsync(request, cancellationToken);
        }

        public static class User
        {
            public static string Id = "9185B0FE-0420-41D5-8C7C-07C818D53758";
            public static string KullaniciAdi = "Mehmet";
            public static string Sifre = "1234";
            public static string Role = "A";
        }
    }
}