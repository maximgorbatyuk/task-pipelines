﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Controllers
{
    [ApiController]
    [Route("api/pipelines")]
    [AllowAnonymous]
    public class PipelineController : ControllerBase
    {
        private readonly PipelineService _service;

        public PipelineController(PipelineService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public Task<IReadOnlyCollection<PipelineResponse>> AllAsync() => _service.AllAsync();

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync()
        {
            return Ok(new
            {
                id = await _service.CreateAsync()
            });
        }

        [HttpGet("{id}")]
        public Task<PipelineResponse> GetAsync(string id) => _service.GetAsync(id);

        [HttpGet("{id}/start")]
        public Task StartAsync(string id) => _service.StartAsync(id);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}