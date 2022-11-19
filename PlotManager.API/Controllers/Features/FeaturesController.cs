using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlotManager.API.DTOs;
using PlotManager.Application.Services;
using PlotManager.Contracts;
using PlotManager.Contracts.Feature;

namespace PlotManager.API.Controllers.Features
{
    [Authorize]
    [ApiController]
    [Route("api/features")]
    public class FeaturesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FeaturesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet(Name = nameof(GetFeatures))]
        public async Task<ActionResult<PagedList<FeatureDTO>>> GetFeatures([FromQuery] FeatureResourceParameters featureResourceParameters)
        {
            var features = await _serviceManager.FeatureService.GetAsync(featureResourceParameters);
            if (features.Count() == 0)
            {
                return NotFound();
            }
            var paginationMetadata = features.GetMetadata();
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));
            return Ok(features);            
            //var links = CreateLinksForFeatures(featureResourceParameters, features.HasNext, features.HasPrevious);
            //var linkedCollection = new { value = features, navigationLinks = links };
            //return Ok(linkedCollection);
        }

        [HttpGet("{featureId:guid}", Name = nameof(GetFeatureById))]
        public async Task<ActionResult<FeatureDTO>> GetFeatureById(Guid featureId)
        {
            var result = await _serviceManager.FeatureService.GetByIdAsync(featureId);
            return Ok(result);
        }

        [HttpPost(Name = nameof(CreateFeature))]
        public async Task<ActionResult> CreateFeature(FeatureForCreationDTO featureForCreationDTO)
        {
            var createdFeature = await _serviceManager.FeatureService.CreateAsync(featureForCreationDTO);
            return CreatedAtRoute(nameof(GetFeatureById), new { featureId = createdFeature.Id }, createdFeature);
        }

        [HttpPatch("{featureId:guid}", Name = nameof(UpdateFeautre))]
        public async Task<ActionResult> UpdateFeautre(Guid featureId, FeatureForUpdateDTO featureForUpdateDTO)
        {
            await _serviceManager.FeatureService.UpdateAsync(featureId, featureForUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{featureId:guid}", Name = nameof(DeleteFeature))]
        public async Task<ActionResult> DeleteFeature(Guid featureId)
        {
            await _serviceManager.FeatureService.DeleteAsync(featureId);
            return NoContent();
        }

        private List<LinkDTO> CreateLinksForFeatures(FeatureResourceParameters featureResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDTO>();

            links.Add(new LinkDTO(CreateFeaturesResourceUri(featureResourceParameters, ResourceUriType.Current),
                                  "self",
                                  "GET"));

            if (hasNext)
            {
                links.Add(new LinkDTO(CreateFeaturesResourceUri(featureResourceParameters, ResourceUriType.NextPage),
                      "nextPage",
                      "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new LinkDTO(CreateFeaturesResourceUri(featureResourceParameters, ResourceUriType.PreviousPage),
                      "previousPage",
                      "GET"));
            }

            return links;
        }

        private List<LinkDTO> CreateLinksForFeature(Guid featureId)
        {
            var links = new List<LinkDTO>();

            links.Add(new LinkDTO(Url.Link(nameof(GetFeatureById), new { featureId }),
                      "self",
                      "GET"));

            links.Add(new LinkDTO(Url.Link(nameof(DeleteFeature), new { featureId }),
                      "delete_customer",
                      "DELETE"));

            return links;
        }

        private string CreateFeaturesResourceUri(FeatureResourceParameters featureResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetFeatures),
                        new
                        {
                            name = featureResourceParameters.Name,
                            pageNumber = featureResourceParameters.PageNumber - 1,
                            pageSize = featureResourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetFeatures),
                        new
                        {
                            name = featureResourceParameters.Name,
                            pageNumber = featureResourceParameters.PageNumber + 1,
                            pageSize = featureResourceParameters.PageSize
                        });
                case ResourceUriType.Current:
                default:
                    return Url.Link(nameof(GetFeatures),
                        new
                        {
                            name = featureResourceParameters.Name,
                            pageNumber = featureResourceParameters.PageNumber,
                            pageSize = featureResourceParameters.PageSize
                        });
            }
        }
    }
}