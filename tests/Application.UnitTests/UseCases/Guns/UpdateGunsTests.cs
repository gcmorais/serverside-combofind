using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Update;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.Guns
{
    public class UpdateGunsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IGunsRepository> _gunsRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task ValidGun_UpdateIsCalled_ReturnValidGunResponse()
        {
            // Arrange
            var updateGunRequest = new Fixture().Create<UpdateGunsRequest>();

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
            );

            _gunsRepositoryMock.Setup(repo => repo.GetById(updateGunRequest.Id)).ReturnsAsync(existingGuns);

            _mapperMock.Setup(m => m.Map<GunResponse>(It.IsAny<GunEntity>())).Returns(new GunResponse
            {
                Id = existingGuns.Id,
                Class = updateGunRequest.Class,
                Condition = updateGunRequest.Condition,
                Image = updateGunRequest.Image,
                MainColor = updateGunRequest.MainColor,
                Name = updateGunRequest.Name,
                Quality = updateGunRequest.Quality,
                Type = updateGunRequest.Type,
                AveragePrice = updateGunRequest.AveragePrice,
            });

            var updateGunHandler = new UpdateGunsHandler(
                _unitOfWorkMock.Object,
                _gunsRepositoryMock.Object,
                _mapperMock.Object
            );

            // Act
            var response = await updateGunHandler.Handle(updateGunRequest, new CancellationToken());

            // Assert
            response.ShouldNotBeNull();
            response.Class.ShouldBe(updateGunRequest.Class);
            response.Condition.ShouldBe(updateGunRequest.Condition);
            response.Image.ShouldBe(updateGunRequest.Image);
            response.MainColor.ShouldBe(updateGunRequest.MainColor);
            response.Name.ShouldBe(updateGunRequest.Name);
            response.Quality.ShouldBe(updateGunRequest.Quality);
            response.Type.ShouldBe(updateGunRequest.Type);
            response.AveragePrice.ShouldBe(updateGunRequest.AveragePrice);
        }
    }
}
