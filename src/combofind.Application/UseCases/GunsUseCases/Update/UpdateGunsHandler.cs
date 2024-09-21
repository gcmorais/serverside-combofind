using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Interface;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Update
{
    public class UpdateGunsHandler : IRequestHandler<UpdateGunsRequest, GunResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGunsRepository _gunsRepository;
        private readonly IMapper _mapper;

        public UpdateGunsHandler(IUnitOfWork unitOfWork, IGunsRepository gunsRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _gunsRepository = gunsRepository;
            _mapper = mapper;
        }
        public async Task<GunResponse> Handle(UpdateGunsRequest request, CancellationToken cancellationToken)
        {
            var data = await _gunsRepository.GetById(request.Id);

            if (data == null)
            {
                return default;
            }

            data.UpdateData(request.Name, request.Type, request.Quality, request.Class, request.Condition, request.MainColor, request.AveragePrice, request.Image);

            _gunsRepository.Update(data);

            await _unitOfWork.Commit();

            return _mapper.Map<GunResponse>(data);
        }
    }
}
