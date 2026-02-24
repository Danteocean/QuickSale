using QuickSale.Business.DTOs;
using QuickSale.Business.Services;
using System;
using System.Linq;
using System.Web.Http;

namespace QuickSale.Application.Controllers
{
    [RoutePrefix("api/sales")]
    public class SalesController : ApiController
    {
        private readonly SaleService _saleService = new SaleService();

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSale([FromBody] SaleRequestDto request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest("La solicitud de venta es inválida o no tiene items.");

            try
            {
                return Ok(new { SaleId = _saleService.ProcessSale(request), Message = "Venta creada exitosamente" });
            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSale(int id)
        {
            try
            {
                var sale = _saleService.GetSaleById(id);
                if (sale == null)
                {
                    return NotFound();
                }
                return Ok(_saleService.GetSaleById(id));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public IHttpActionResult GetSalesByCustomer(int customerId)
        {
            try
            {
                var history = _saleService.GetSalesByCustomerId(customerId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
