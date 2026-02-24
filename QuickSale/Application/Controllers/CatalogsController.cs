using QuickSale.Business.Services;
using System;
using System.Web.Http;

namespace QuickSale.Application.Controllers
{
    [RoutePrefix("api")]
    public class CatalogsController : ApiController
    {
        private readonly CatalogService _catalogService = new CatalogService();

        [HttpGet]
        [Route("products")]
        public IHttpActionResult GetProducts()
        {
            try
            {
                return Ok(_catalogService.GetActiveProducts());
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("customers")]
        public IHttpActionResult GetCustomers()
        {
            try
            {
                return Ok(_catalogService.GetActiveCustomers());
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
