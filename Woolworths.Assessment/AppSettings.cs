using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Woolworths.Assessment
{
    [ExcludeFromCodeCoverage]
    public class AppSettings
    {
        private readonly IConfiguration _appSettings;
        public AppSettings(IConfiguration configuration)
        {
            _appSettings = configuration.GetSection("App");
        }

        public string Token()
        {
            return _appSettings["Token"];
        }

        public string UserName()
        {
            return _appSettings["UserName"];
        }

        public string WoolworthsResourceUrl()
        {
            return _appSettings["ResourceUrl"];
        }
    }
}