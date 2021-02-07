using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VsRemote.Interfaces;
using VsRemote.Models;

namespace VsRemote.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly IVisualStudioService _visualStudioService;

        private readonly ILogger<VisualStudioController> _logger;
        public SolutionController(IVisualStudioService visualStudioService,
                                  ILogger<VisualStudioController> logger)
        {
            _visualStudioService = visualStudioService;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        // Used for swagger documentation
        [ProducesResponseType(typeof(VsSolution), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                _logger.LogInformation("Get solution called");
                var solutionDetails = await _visualStudioService.GetSolutionDetails(id);
                return solutionDetails == null ? NotFound() : (IActionResult)Ok(solutionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return BadRequest("Opps something is wrong! Check the logs");
        }

        [HttpPost("{id:int}")]
        [ProducesResponseType(typeof(VsResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(int id)
        {
            try
            {
                var result = await _visualStudioService.StartBuildAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }

            return BadRequest("Opps something is wrong! Check the logs");
        }
    }
}
