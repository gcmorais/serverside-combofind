using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Delete;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.Guns
{
    public class DeleteGunsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IGunsRepository> _gunsRepositoryMock;

        public DeleteGunsTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _gunsRepositoryMock = new Mock<IGunsRepository>();
        }

        [Fact]
        public async Task GunExists_DeleteIsCalled_ReturnValidGunsResponse()
        {
            // Arrange
            var deleteGunRequest = new Fixture().Create<DeleteGunsRequest>();

            var existingGuns = new GunEntity(
                name: "Luvas de Motorista (★) | Xadrez Imperial",
                type: "Driver Gloves",
                quality: "Extraordinário",
                classType: "Luvas",
                condition: "Veterana de Guerra",
                mainColor: "Purple",
                averagePrice: 1331,
                image: "https://example.com/image.png",
                collection: new Collection("purple", "low")
            )
            {
                Id = deleteGunRequest.Id
            };

            // Setup mocks
            _gunsRepositoryMock
                .Setup(repo => repo.GetById(deleteGunRequest.Id))
                .ReturnsAsync(existingGuns);

            _gunsRepositoryMock
                .Setup(repo => repo.Delete(existingGuns))
                .Verifiable();

            _mapperMock.Setup(m => m.Map<GunResponse>(It.IsAny<GunEntity>())).Returns(new GunResponse
            {
                Id = existingGuns.Id,
                Class = existingGuns.Class,
                Condition = existingGuns.Condition,
                Image = existingGuns.Image,
                MainColor = existingGuns.MainColor,
                Name = existingGuns.Name,
                Quality = existingGuns.Quality,
                Type = existingGuns.Type,
                AveragePrice = existingGuns.AveragePrice,
            });

            var gunDeleteData = new DeleteGunsHandler(
                _unitOfWorkMock.Object,
                _gunsRepositoryMock.Object,
                _mapperMock.Object
            );

            // Act
            var response = await gunDeleteData.Handle(deleteGunRequest, new CancellationToken());

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(deleteGunRequest.Id);
        }

        [Fact]
        public async Task GunDoesNotExist_DeleteIsCalled_ReturnDefault()
        {
            // Arrange
            var deleteGunRequest = new Fixture().Create<DeleteGunsRequest>();

            _gunsRepositoryMock
                .Setup(repo => repo.GetById(deleteGunRequest.Id))
                .ReturnsAsync((GunEntity)null);

            var gunDeleteData = new DeleteGunsHandler(_unitOfWorkMock.Object, _gunsRepositoryMock.Object, _mapperMock.Object);

            // Act
            var response = await gunDeleteData.Handle(deleteGunRequest, new CancellationToken());

            // Assert
            response.ShouldBeNull();

            _gunsRepositoryMock.Verify(repo => repo.Delete(It.IsAny<GunEntity>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }
    }
}
