using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace QuickSale.Application.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public System.Net.Http.HttpResponseMessage Index()
        {
            var response = Request.CreateResponse(System.Net.HttpStatusCode.Moved);
            response.Headers.Location = new System.Uri(Request.RequestUri, "swagger/ui/index");
            return response;
        }
    }
}
