using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.Models;
using TTechTash.Domain.Repository.Abstraction;
using TTechTash.Domain.Services.Abstraction;
using TTechTask.Domain.DTOs;

namespace TTechTask.Services.Servives
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainServices _mainServices;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,IMainServices mainServices,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mainServices = mainServices;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<ProductDTO.Get>>> GetAllProducts()
        {
            var response = new ApiResponse<List<ProductDTO.Get>>();
            var products = _unitOfWork.ProductRepoistory.Get().ToList()
                .Select(_mapper.Map<ProductDTO.Get>).ToList();
            if(products.Count() == 0 || products == null)
            {
                response.Status = false;
                response.Message = "There Is No Products";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = products;
            return response;
        }
        public async Task<ApiResponse<ProductDTO.Get>> GetProductById(int Id)
        {
            var response = new ApiResponse<ProductDTO.Get>();
            var product = await _unitOfWork.ProductRepoistory.Get(Id);
            if(product == null)
            {
                response.Status = false;
                response.Message = "Wrong Product Id";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<ProductDTO.Get>(product);
            return response;
        }
        public async Task<ApiResponse<ProductDTO.Get>> AddProduct(ProductDTO.Add model)
        {
            var response = new ApiResponse<ProductDTO.Get>();
            var product = _mapper.Map<Product>(model);
            if(model.ImageFile != null)
            {
                product.Image = _mainServices.UploadPhoto(model.ImageFile);
            }
            product = await _unitOfWork.ProductRepoistory.Add(product);
            if(product == null)
            {
                response.Status = false;
                response.Message = "Failed To Add";
                return response;
            }
            await _unitOfWork.Save();
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<ProductDTO.Get>(product);
            return response;
        }
        public async Task<ApiResponse<ProductDTO.Get>> EditProduct(ProductDTO.Edit model)
        {
            var response = new ApiResponse<ProductDTO.Get>();
            var product = await _unitOfWork.ProductRepoistory.Get(model.Id);
            if(product == null)
            {
                response.Status = false;
                response.Message = "Wrong Product Id";
                return response;
            }
            product = _mapper.Map<Product>(model);
            if(model.ImageFile != null)
            {
                _mainServices.DeletePhoto(model.Image);
                product.Image = _mainServices.UploadPhoto(model.ImageFile);
            }
            product = _unitOfWork.ProductRepoistory.Update(product);
            if(product == null)
            {
                response.Status = false;
                response.Message = "Failed To Edit";
                return response;
            }
            await _unitOfWork.Save();
            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<ProductDTO.Get>(product);
            return response;
        }
        public async Task<ApiResponse<ProductDTO.Get>> DeleteProduct(int Id)
        {
            var response = new ApiResponse<ProductDTO.Get>();
            var product = await _unitOfWork.ProductRepoistory.Get(Id);
            if (product == null)
            {
                response.Status = false;
                response.Message = "Wrong Product Id";
                return response;
            }
            if(product.Image != null)
            {
                _mainServices.DeletePhoto(product.Image);
            }
            _unitOfWork.ProductRepoistory.Remove(product);
            await _unitOfWork.Save();

            response.Status = true;
            response.Message = "Success";
            response.Response = _mapper.Map<ProductDTO.Get>(product);
            return response;
        }
    }
}
