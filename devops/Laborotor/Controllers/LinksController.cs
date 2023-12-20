using Laborotor.Models.Requests;
using Laborotor.Services.LinksService.Common;
using Microsoft.AspNetCore.Mvc;

namespace Laborotor.Controllers
{
    [Route("[controller]")]
    public class LinksController: ControllerBase
    {
        private readonly ILinksService _linksService;
        public LinksController(ILinksService linksService) 
        {
            _linksService = linksService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            try
            {
                var res = await _linksService.Get(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(id);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LinkCreateRequest linkCreateRequest)
        {
            try
            {
                await _linksService.Create(linkCreateRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Create([FromBody] StatusUpdateRequest linkCreateRequest)
        {
            try
            {
                await _linksService.UpdateStatus(linkCreateRequest.Id, linkCreateRequest.Status, linkCreateRequest.Code);
                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
