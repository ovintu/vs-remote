using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsRemote.Models;
using VsRemote.Interfaces;
using System.Net.Mime;

namespace VsRemote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisualStudioController : ControllerBase
    {
        private readonly IVisualStudioService _visualStudioService;

        private readonly ILogger<VisualStudioController> _logger;
        public VisualStudioController(IVisualStudioService visualStudioService,
                                      ILogger<VisualStudioController> logger)
        {
            _visualStudioService = visualStudioService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("Get visual instances called");
                IEnumerable<VisualStudioInstance> vsInstances = await _visualStudioService.GetRunningInstancesAsync();
                return Ok(vsInstances);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return BadRequest();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IAsyncResult> StartBuildAsync(string vsInstance)
        {
            await Task.Delay(1);
            return Task.FromResult(VsResult.Success);
        }
    }
}
