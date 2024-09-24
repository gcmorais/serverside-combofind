using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.Update;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.CollectionsUseCases
{
    public class UpdateCollectionsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICollectionRepository> _collectionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateCollectionsTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _collectionRepositoryMock = new Mock<ICollectionRepository>();
        }

        [Fact]
        public async Task ValidCollection_UpdateIsCalled_ReturnValidResponseCollection()
        {
            // Arrange
            var updateCollectionRequest = new Fixture().Create<UpdateCollectionRequest>();

            var collectionID= Guid.NewGuid();

            var existingCollection = new Collection(
                budget: "Low",
                color: "Red"
            )
            {
                Id = updateCollectionRequest.Id,
                DateCreated = DateTimeOffset.UtcNow
            };

            var updateCollection = new Collection(
                budget: updateCollectionRequest.Budget,
                color: updateCollectionRequest.Color
            )
            {
                Id = existingCollection.Id,
                DateCreated = existingCollection.DateCreated,
                DateUpdated = DateTimeOffset.UtcNow
            };

            var cancellationToken = new CancellationToken();

            _collectionRepositoryMock
                .Setup(repo => repo.GetById(updateCollectionRequest.Id))
                .ReturnsAsync(existingCollection);

            _collectionRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<Collection>()))
                .Callback<Collection>(c =>
                {
                    c.ShouldNotBeNull();
                    c.Color.ShouldBe(updateCollectionRequest.Color);
                    c.Budget.ShouldBe(updateCollectionRequest.Budget);
                });

            _mapperMock.Setup(m => m.Map<CollectionResponse>(It.IsAny<Collection>())).Returns(new CollectionResponse
            {
                Budget = updateCollectionRequest.Budget,
                Color = updateCollectionRequest.Color
            });

            var collectionUpdateService = new UpdateCollectionHandler(_unitOfWorkMock.Object, _collectionRepositoryMock.Object, _mapperMock.Object);

            // Act
            var response = await collectionUpdateService.Handle(updateCollectionRequest, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Budget.ShouldBe(updateCollectionRequest.Budget);
            response.Color.ShouldBe(updateCollectionRequest.Color);

            _collectionRepositoryMock.Verify(repo => repo.Update(It.Is<Collection>(c =>
                c.Id == updateCollectionRequest.Id &&
                c.Color == updateCollectionRequest.Color &&
                c.Budget == updateCollectionRequest.Budget)), Times.Once);

            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
