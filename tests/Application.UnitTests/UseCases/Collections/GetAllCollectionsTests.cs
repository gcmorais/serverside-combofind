using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.GetAll;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.CollectionsUseCases
{
    public class GetAllCollectionsTests
    {
        private readonly Mock<ICollectionRepository> _collectionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllCollectionsTests()
        {
            _mapperMock = new Mock<IMapper>();
            _collectionRepositoryMock = new Mock<ICollectionRepository>();
        }

        [Fact]
        public async Task ValidCollection_GetAllIsCalled_ReturnValidResponseCollection()
        {
            // Arrange
            var collections = new List<Collection>
            {
                new Collection(budget: "High", color: "Red"),
                new Collection(budget: "Low", color: "Blue"),
            };

            var collectionResponse = new List<CollectionResponse>
            {
                new CollectionResponse { Budget = "High", Color = "Red" },
                new CollectionResponse { Budget = "Low", Color = "Blue" }
            };

            var cancellationToken = new CancellationToken();

            _collectionRepositoryMock
                .Setup(repo => repo.GetAllCollections())
                .ReturnsAsync(collections);

            _mapperMock
                .Setup(m => m.Map<List<CollectionResponse>>(It.IsAny<List<Collection>>()))
                .Returns(collectionResponse);

            var getAllCollectionsHandler = new GetAllCollectionHandler(_collectionRepositoryMock.Object, _mapperMock.Object);

            // Act
            var response = await getAllCollectionsHandler.Handle(new GetAllCollectionRequest(), cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBe(collectionResponse);

            _collectionRepositoryMock.Verify(repo => repo.GetAllCollections(), Times.Once);
            _mapperMock.Verify(m => m.Map<List<CollectionResponse>>(It.IsAny<List<Collection>>()), Times.Once);
        }

    }
}
