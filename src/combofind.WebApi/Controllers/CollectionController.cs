using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace combofind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CollectionResponse>> Create(CreateCollectionRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

    }
}
