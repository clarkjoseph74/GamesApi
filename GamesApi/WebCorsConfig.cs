using System.Web.Http;
using System.Web.Http.Cors;

namespace GamesApi.Api;

public class Startup
{
    // Other methods...

    public void ConfigureServices(IServiceCollection services)
    {
        // Add CORS
       

        // Other service configurations...
    }

    public void Configure(IApplicationBuilder app)
    {
        // Use CORS
        app.UseCors();

        // Other app configurations...
    }
}