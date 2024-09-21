using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Interface;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Delete
{
    public class DeleteGunsHandler : IRequestHandler<DeleteGunsRequest, GunResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGunsRepository _gunsRepository;
        private readonly IMapper _mapper;

        public DeleteGunsHandler(IUnitOfWork unitOfWork, IGunsRepository gunsRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _gunsRepository = gunsRepository;
            _mapper = mapper;
        }
        public async Task<GunResponse> Handle(DeleteGunsRequest request, CancellationToken cancellationToken)
        {
            var data = await _gunsRepository.GetById(request.Id);

            if (data == null) return default;

            _gunsRepository.Delete(data);
            await _unitOfWork.Commit();

            return _mapper.Map<GunResponse>(data);
        }
    }
}
