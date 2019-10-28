using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subscribes.Domain.Models;
using Subscribes.Domain.Services;
using Subscribes.Domain.Repositories;
using Subscribes.Domain.Services.Communication;

namespace Subscribes.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscribeService(ISubscribeRepository subscribeRepository, IUnitOfWork unitOfWork)
        {
            _subscribeRepository = subscribeRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Subscribe>> ListAsync()
        {
            return await _subscribeRepository.ListAsync();
        }

        public async Task<SubscribeResponse> SaveAsync(Subscribe subscribe)
        {
            try
            {
                //try to add the new subscribe to the database
                await _subscribeRepository.AddAsync(subscribe);
                //API try to save it
                await _unitOfWork.CompleteAsync();
                
                return new SubscribeResponse(subscribe);
            }
            catch (Exception ex)
            {
                //API calls some fictional logging service and return a response indicating failure
                return new SubscribeResponse($"An error occurred when saving the subscribe: {ex.Message}");
            }
        }
        

        public async Task<SubscribeResponse> GetByIdAsync(int id)
        {
            var existingSubscribe = await _subscribeRepository.FindByIdAsync(id);

            if (existingSubscribe == null)
            {
                return new SubscribeResponse("Subscribe not found");
            }
            
            return new SubscribeResponse(existingSubscribe);
        }

        public async Task<SubscribeResponse> DeleteAsync(int id)
        {
            var existingSubscribe = await _subscribeRepository.FindByIdAsync(id);

            if (existingSubscribe == null)
            {
                return new SubscribeResponse("Subscribe not found");
            }

            try
            {
                _subscribeRepository.Remove(existingSubscribe);
                await _unitOfWork.CompleteAsync();
                
                return new SubscribeResponse(existingSubscribe);
            }
            catch (Exception ex)
            {
                return new SubscribeResponse($"An error occurred when deleting the subscribe: {ex.Message}");
            }
        }

        public async Task<SubscribeResponse> UpdateAsync(int id)
        {
            var existingSubscribe = await _subscribeRepository.FindByIdAsync(id);
            
            if (existingSubscribe == null)
            {
                return new SubscribeResponse("Subscribe not found");
            }

            existingSubscribe.DataEnd = existingSubscribe.DataEnd.AddYears(1);
            
            try
            {
                _subscribeRepository.Update(existingSubscribe);
                await _unitOfWork.CompleteAsync();
                
                return new SubscribeResponse(existingSubscribe);
            }
            catch (Exception ex)
            {
                return new SubscribeResponse($"An error occurred when updating the subscribe: {ex.Message}");
            }
        }
    }
}