using AutoMapper;
using combofind.Domain.Interface;
using Moq;

namespace Application.UnitTests.UseCases.Guns
{
    public class GetAllGunsTests
    {
        private readonly Mock<IGunsRepository> _gunsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllGunsTests()
        {
            _gunsRepositoryMock = new Mock<IGunsRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GunsExists_DeleteIsCalled_ReturnValidGunsResponse()
        {

        }
    }
}
