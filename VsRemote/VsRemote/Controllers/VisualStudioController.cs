using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSRemote.Interfaces;
using VSRemote.Models;

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
        public async Task<IEnumerable<VisualStudioInstance>> GetAsync()
        {
            _logger.LogInformation("Get visual instances called");
            IEnumerable<VisualStudioInstance> vsInstances = await _visualStudioService.GetRunningInstancesAsync();
            return vsInstances;
        }

        [HttpPost]
        public async Task<IAsyncResult> StartBuildAsync(string vsInstance)
        {
            await Task.Delay(1);
            return Task.FromResult(VSResult.Success);
        }
    }
}
