using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlotManager.API.Controllers.Features;
using PlotManager.Application.Services;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;
using PlotManager.Contracts.PlotComplex;
using PlotManager.Domain.Entities;

namespace PlotManager.API.Controllers.PlotComplexes
{
    [ApiController]
    [Authorize]
    [Route("api/plotcomplexes")]
    public class PlotComplexesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger _Logger;

        public PlotComplexesController(IServiceManager serviceManager, ILogger<FeaturesController> logger)
        {
            _serviceManager = serviceManager;
            _Logger = logger;
        }

        [HttpGet(Name = nameof(GetPlotComplexes))]
        public async Task<ActionResult<PagedList<PlotComplexDTO>>> GetPlotComplexes([FromQuery] PlotComplexResourceParameters plotComplexResourceParameters)
        {
            var plotComplexes = await _serviceManager.PlotComplexService.GetAsync(plotComplexResourceParameters);
            if (plotComplexes.Count == 0)
            {
                return NotFound();
            }
            var paginationMetadata = plotComplexes.GetMetadata();
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));
            //TODO ADD LINKS
            return Ok(plotComplexes);
        }

        [HttpGet("{plotComplexId:guid}", Name = nameof(GetPlotComplexById))]
        public async Task<ActionResult<PlotComplexDTO>> GetPlotComplexById(Guid plotComplexId)
        {
            var result = await _serviceManager.PlotComplexService.GetByIdAsync(plotComplexId);
            return Ok(result);
        }

        [HttpPost(Name = nameof(CreatePlotComplex))]
        public async Task<ActionResult> CreatePlotComplex(PlotComplexForCreationDTO plotComplexForCreationDTO)
        {
            var createdPlotComplex = await _serviceManager.PlotComplexService.CreateAsync(plotComplexForCreationDTO);
            return CreatedAtRoute(nameof(GetPlotComplexById), new { plotComplexId = createdPlotComplex.Id }, createdPlotComplex);
        }

        [HttpPatch("{plotComplexId:guid}", Name = nameof(UpdatePlotComplex))]
        public async Task<ActionResult> UpdatePlotComplex(Guid plotComplexId, PlotComplexForUpdateDTO plotComplexForUpdateDTO)
        {
            await _serviceManager.PlotComplexService.UpdateAsync(plotComplexId, plotComplexForUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{plotComplexId:guid}", Name = nameof(DeletePlotComplex))]
        public async Task<ActionResult> DeletePlotComplex(Guid plotComplexId)
        {
            await _serviceManager.PlotComplexService.DeleteAsync(plotComplexId);
            return NoContent();
        }
    }
}
