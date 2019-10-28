using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Subscribes.Domain.Models;
using Subscribes.Domain.Services;
using Subscribes.Resources;
using Subscribes.Extensions;

namespace Subscribes.Controllers
{
    [Route("/api/[controller]")]
    public class SubscribesController : ControllerBase
    {
        private readonly ISubscribeService _subscribeService;
        private readonly IMapper _mapper;

        public SubscribesController(ISubscribeService subscribeService, IMapper mapper)
        {
            _subscribeService = subscribeService;
            _mapper = mapper;
        }

        //get all
        [HttpGet]
        public async Task<IEnumerable<SubscribeResource>> GetAllAsync()
        {
            var subscribes = await _subscribeService.ListAsync();
            // map return data 
            var resources = _mapper.Map<IEnumerable<Subscribe>, IEnumerable<SubscribeResource>>(subscribes);
            return resources;
        }

        // post
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscribeResource resource)
        {
            // validating the request data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            resource.DataStart = DateTime.Now;
            resource.DataEnd = resource.DataStart.AddYears(1);

            //  mapping the resource to our model
            var subscribe = _mapper.Map<SaveSubscribeResource, Subscribe>(resource);
            
            // get result from model (return response)
            var result = await _subscribeService.SaveAsync(subscribe);

            // API returns a bad request
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            // API maps the new category (now including data such as the new Id) to our previously created SubscribeResource
            // get subscribe from response
            var subscribeResource = _mapper.Map<Subscribe, SubscribeResource>(result.Subscribe);
            // sends it to the client
            return Ok(subscribeResource);
        }
        
        
        // get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _subscribeService.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var subscribeResource = _mapper.Map<Subscribe, SubscribeResource>(result.Subscribe);
            return Ok(subscribeResource);
        }
        
        //delete by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _subscribeService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var subscribeResource = _mapper.Map<Subscribe, SubscribeResource>(result.Subscribe);

            return Ok(subscribeResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id)
        {
            var result = await _subscribeService.UpdateAsync(id);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Subscribe, SubscribeResource>(result.Subscribe);
            return Ok(productResource);
        }
    }
}