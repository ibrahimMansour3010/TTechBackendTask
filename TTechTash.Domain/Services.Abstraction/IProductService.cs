using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTask.Domain.DTOs;

namespace TTechTash.Domain.Services.Abstraction
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductDTO.Get>>> GetAllProducts();
        Task<ApiResponse<ProductDTO.Get>> GetProductById(int Id);
        Task<ApiResponse<ProductDTO.Get>> AddProduct(ProductDTO.Add model);
        Task<ApiResponse<ProductDTO.Get>> EditProduct(ProductDTO.Edit model);
        Task<ApiResponse<ProductDTO.Get>> DeleteProduct(int Id);
    }
}
