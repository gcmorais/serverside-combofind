﻿using combofind.Application.Models;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Create;
using combofind.Application.UseCases.GunsUseCases.Delete;
using combofind.Application.UseCases.GunsUseCases.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace combofind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GunsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GunsController(IMediator mediator)
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

        [HttpPost]
        public async Task<ActionResult<ResponseModel<GunResponse>>> Create(CreateGunRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return HandleResponse(response, "Gun not found.", "Item foi registrado com sucesso!");
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<GunResponse>>> Update(UpdateGunsRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return HandleResponse(response, "Collection not found.", "Item foi atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<GunResponse>>> Delete(Guid? id, CancellationToken cancellationToken)
        {
            if (id is null) return BadRequest();

            var deleteRequest = new DeleteGunsRequest(id.Value);
            var response = await _mediator.Send(deleteRequest, cancellationToken);
            return HandleResponse(response, "Guns not found.", "Item deletado com sucesso!");
        }
    }
}
