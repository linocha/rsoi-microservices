using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Models;
using Products.Domain.Services;
using Products.Domain.Repositories;
using Products.Domain.Services.Communication;

namespace Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                //try to add the new product to the database
                await _productRepository.AddAsync(product);
                //API try to save it
                await _unitOfWork.CompleteAsync();
                
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                //API calls some fictional logging service and return a response indicating failure
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var pResp = await GetByIdAsync(id);
            if (!pResp.Success)
            {
                return new ProductResponse("Product not found");
            }

            var existingProduct = pResp.Product;
            
//            var existingProduct = await _productRepository.FindByIdAsync(id);
//
//            if (existingProduct == null)
//            {
//                return new ProductResponse("Product not found");
//            }

            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();
                
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found");
            }
            
            return new ProductResponse(existingProduct);
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
            {
                return new ProductResponse("Product not found");
            }

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();
                
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}