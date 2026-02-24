using System.Web.Http;
using WebActivatorEx;
using QuickSale;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace QuickSale
{
    public class SwaggerConfig
    {
        // En App_Start/SwaggerConfig.cs
        public static void Register()
        {
            var fileName = "QuickSale.xml"; 
            GlobalConfiguration.Configuration
                .EnableSwagger(c => {
                    c.SingleApiVersion("v1", "QuickSale API");
                })
                .EnableSwaggerUi(c => {
        });
        }
    }
}
