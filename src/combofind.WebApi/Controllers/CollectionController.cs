using combofind.Application.Models;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.Create;
using combofind.Application.UseCases.CollectionUseCases.Delete;
using combofind.Application.UseCases.CollectionUseCases.GetAll;
using combofind.Application.UseCases.CollectionUseCases.Update;
using combofind.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        private ActionResult<ResponseModel<T>> HandleResponse<T>(T response, string notFoundMessage, string successMessage)
        {
            if (response == null)
            {
                return NotFound(ResponseModel<T>.CreateErrorResponse(notFoundMessage));
            }

            return Ok(ResponseModel<T>.CreateSuccessResponse(response, successMessage));
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<CollectionResponse>>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllCollectionRequest(), cancellationToken);
            return HandleResponse(response, ResourceErrorMessages.NotFound, ResourceSuccessMessages.GetAllMessageSuccess);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CollectionResponse>>> Create(CreateCollectionRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return HandleResponse(response, ResourceErrorMessages.NotFound, ResourceSuccessMessages.CreateMessageSuccess);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ResponseModel<CollectionResponse>>> Update(UpdateCollectionRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return HandleResponse(response, ResourceErrorMessages.NotFound, ResourceSuccessMessages.UpdateMessageSucess);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<CollectionResponse>>> Delete(Guid? id, CancellationToken cancellationToken)
        {
            if (id is null) return BadRequest();

            var deleteCollectionRequest = new DeleteCollectionRequest(id.Value);
            var response = await _mediator.Send(deleteCollectionRequest, cancellationToken);
            return HandleResponse(response, ResourceErrorMessages.NotFound, ResourceSuccessMessages.DeleteMessageSuccess);
        }
    }
}
