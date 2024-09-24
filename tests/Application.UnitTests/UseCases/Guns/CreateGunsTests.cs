using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Create;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.Guns
{
    public class CreateGunsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICollectionRepository> _collectionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IGunsRepository> _gunsRepository;
        public CreateGunsTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gunsRepository = new Mock<IGunsRepository>();
            _mapperMock = new Mock<IMapper>();
            _collectionRepositoryMock = new Mock<ICollectionRepository>();
        }

        [Fact]
        public async Task ValidGun_CreateIsCalled_ReturnValidResponseCollection()
        {
            // Arrange
            var createGunRequest = new Fixture().Create<CreateGunRequest>();
            var collectionID = Guid.NewGuid();

            var collectionData = new Collection(
                budget: "High",
                color: "Red"
            )
            {
                Id = collectionID,
                DateCreated = DateTimeOffset.UtcNow
            };

            var guns = new GunEntity(
                name: "Luvas de Motorista (★) | Xadrez Imperial",
                type: "Driver Gloves",
                quality: "Extraordinário",
                classType: "Luvas",
                condition: "Veterana de Guerra",
                mainColor: "Purple",
                averagePrice: 1331,
                image: "https://steamcommunity-a.akamaihd.net/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DAX1R3LjtQurWzLhRfwP_BcjZ9_NC3nYS0h-LmI7fUqWZU7Mxkh6fF89v32Qfm_xBsZTj3IdKcJwFoaA3XqVS9yOm90JO4uJTNyXUx63Ml-z-DyJQzRVLD/256fx128f",
                collection: collectionData
            );

            var gunResponse = new GunResponse
            {
                Id = guns.Id,
                Class = createGunRequest.Class,
                Condition = createGunRequest.Condition,
                Image = createGunRequest.Image,
                MainColor = createGunRequest.MainColor,
                Name = createGunRequest.Name,
                Quality = createGunRequest.Quality,
                Type = createGunRequest.Type,
                AveragePrice = createGunRequest.AveragePrice,
            };

            _collectionRepositoryMock
                .Setup(repo => repo.GetById(createGunRequest.CollectionData.Id))
                .ReturnsAsync(collectionData);

            _mapperMock
                .Setup(m => m.Map<GunEntity>(createGunRequest))
                .Returns(guns);

            _mapperMock
                .Setup(m => m.Map<GunResponse>(It.IsAny<GunEntity>()))
                .Returns(gunResponse);

            _gunsRepository
                .Setup(repo => repo.Create(It.IsAny<GunEntity>()))
                .Callback<GunEntity>(c =>
                {
                    c.Id.ShouldBe(guns.Id);
                    c.Class.ShouldBe(guns.Class);
                    c.Condition.ShouldBe(guns.Condition);
                    c.Image.ShouldBe(guns.Image);
                    c.MainColor.ShouldBe(guns.MainColor);
                    c.Name.ShouldBe(guns.Name);
                    c.Quality.ShouldBe(guns.Quality);
                    c.Type.ShouldBe(guns.Type);
                    c.AveragePrice.ShouldBe(guns.AveragePrice);
                    c.Collection.ShouldBe(collectionData);
                });

            var createGunHandler = new CreateGunHandler(
                _gunsRepository.Object,
                _collectionRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object
            );

            var cancellationToken = new CancellationToken();

            // Act
            var response = await createGunHandler.Handle(createGunRequest, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Class.ShouldBe(createGunRequest.Class);
            response.Condition.ShouldBe(createGunRequest.Condition);
            response.Image.ShouldBe(createGunRequest.Image);
            response.MainColor.ShouldBe(createGunRequest.MainColor);
            response.Name.ShouldBe(createGunRequest.Name);
            response.Quality.ShouldBe(createGunRequest.Quality);
            response.Type.ShouldBe(createGunRequest.Type);
            response.AveragePrice.ShouldBe(createGunRequest.AveragePrice);

            _collectionRepositoryMock.Verify(repo => repo.GetById(createGunRequest.CollectionData.Id), Times.Once);
            _gunsRepository.Verify(repo => repo.Create(It.IsAny<GunEntity>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
