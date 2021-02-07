using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                _logger.LogInformation("Get solution called");
                var solutionDetails = await _visualStudioService.GetSolutionDetails(id);
                if (solutionDetails == null) 
                    return NotFound();
                return Ok(solutionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.StackTrace);
            }
        }
    }
}
