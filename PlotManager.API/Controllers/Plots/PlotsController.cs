using Microsoft.AspNetCore.Mvc;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Contracts;
using PlotManager.Contracts.Plot;
using PlotManager.Domain.Entities;
using PlotManager.Contracts.Feature;
using Microsoft.AspNetCore.Authorization;
using PlotManager.API.Controllers.Features;
using PlotManager.Application.Services;

namespace PlotManager.API.Controllers.Plots
{
    [ApiController]
    [Authorize]
    [Route("api/plotcomplexes/{plotComplexId:guid}/plots")]
    public class PlotsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _Logger;

        public PlotsController(IServiceManager serviceManager, ILogger<FeaturesController> logger)
        {
            _serviceManager = serviceManager;
            _Logger = logger;
        }


        [HttpGet(Name = nameof(GetPlots))]
        public async Task<ActionResult<PagedList<PlotDTO>>> GetPlots(Guid plotComplexId, [FromQuery] PlotResourceParameters plotResourceParameters)
        {
            var plots = await _serviceManager.PlotService.GetAsync(plotComplexId, plotResourceParameters);
            if (plots.Count == 0)
            {
                return NotFound();
            }
            var paginationMetadata = plots.GetMetadata();
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));
            //TODO ADD LINKS
            return Ok(plots);
        }

        [HttpGet("{plotId:guid}", Name = nameof(GetPlotById))]
        public async Task<ActionResult<PlotDTO>> GetPlotById(Guid plotId)
        {
            var result = await _serviceManager.PlotService.GetByIdAsync(plotId);

            return Ok(result);
        }

        [HttpPost(Name = nameof(CreatePlot))]
        public async Task<ActionResult> CreatePlot(Guid plotComplexId, PlotForCreationDTO plotForCreationDTO)
        {
            var createdPlot = await _serviceManager.PlotService.CreateAsync(plotComplexId, plotForCreationDTO);
            return CreatedAtRoute(nameof(GetPlotById), new { plotComplexId, plotId = createdPlot.Id }, createdPlot);

        }

        [HttpPatch("{plotId:guid}", Name = nameof(UpdatePlot))]
        public async Task<ActionResult> UpdatePlot(Guid plotId, PlotForUpdateDTO plotForUpdateDTO)
        {
            await _serviceManager.PlotService.UpdateAsync(plotId, plotForUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{plotId:guid}", Name = nameof(DeletePlot))]
        public async Task<ActionResult> DeletePlot(Guid plotId)
        {
            await _serviceManager.PlotService.DeleteAsync(plotId);
            return NoContent();
        }
    }
}