using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTechTash.Domain.Services.Abstraction;
using TTechTask.Domain.DTOs;

namespace TTechTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles ="User")]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _productService.GetAllProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message});
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                return Ok(await _productService.GetProductById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message});
            }
        }
        [Authorize(Roles = "Admin,Tester")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm]ProductDTO.Add model)
        {
            try
            {
                return Ok(await _productService.AddProduct(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message});
            }
        }
        [Authorize(Roles = "Admin,Tester")]
        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditProduct([FromForm]ProductDTO.Edit model)
        {
            try
            {
                return Ok(await _productService.EditProduct(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message});
            }
        }
        [Authorize(Roles = "Admin,Tester")]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                return Ok(await _productService.DeleteProduct(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message});
            }
        }
    }
}
