using Domain.Utils.HttpStatusExceptionCustom;
using Domain.Views;
using DomainProduct.Interfaces.IServices;
using DomainProduct.Views.ProductViews;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServices;
        public ProductController(IProductService productServices)
        {
            _productServices = productServices;
        }

        [Produces("application/json")]
        [HttpPost("/api/AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddView product)
        {
            try
            {
                var returnProduct = await _productServices.Add(product);
                return Created("Produto Adicionado com sucesso!",returnProduct);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }

        [Produces("application/json")]
        [HttpPut("/api/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateView product)
        {
            try
            {
                var returnProduct = await _productServices.Update(product);
                return Ok(returnProduct);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPost("/api/GetByCode")]
        public async Task<IActionResult> GetByCode([FromQuery] int codigo)
        {
            try
            {
                var response = await _productServices.GetByCode(codigo);
                return Ok(response);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPost("/api/ListProductByDateManufactureWhitPagination")]
        public async Task<IActionResult> ListProductByDateManufactureWhitPagination([FromBody] PaginationsControllersView paginations)
        {
            try
            {
                var returnProduct = await _productServices.ListProductByDateManufactureWhitPagination(paginations.StartDate, paginations.EndDate, paginations.PageNumber, paginations.PageSize);
                return Ok(returnProduct);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("/api/ListAllProducts")]
        public async Task<IActionResult> ListAllProducts()
        {
            try
            {
                var products = await _productServices.GetAll();
                return Ok(products);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpDelete("/api/Delete")]
        public async Task<IActionResult> GetByNewOrderId([FromQuery] int codigo)
        {
            try
            {
                var response = await _productServices.DeleteProduct(codigo);
                return Ok(response);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
