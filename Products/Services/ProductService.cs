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

        public async Task<SaveProductResponse> SaveAsync(Product product)
        {
            try
            {
                //try to add the new product to the database
                await _productRepository.AddAsync(product);
                //API try to save it
                await _unitOfWork.CompleteAsync();
                
                return new SaveProductResponse(product);
            }
            catch (Exception ex)
            {
                //API calls some fictional logging service and return a response indicating failure
                return new SaveProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }
    }
}