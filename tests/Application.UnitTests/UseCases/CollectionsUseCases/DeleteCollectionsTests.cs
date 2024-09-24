using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.Delete;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.CollectionsUseCases
{
    public class DeleteCollectionsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICollectionRepository> _collectionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public DeleteCollectionsTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _collectionRepositoryMock = new Mock<ICollectionRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task ValidCollection_DeleteIsCalled_ReturnValidResponseCollection()
        {
            // Arrange
            var deleteCollectionRequest = new Fixture().Create<DeleteCollectionRequest>();

            var collection = new Collection(
               color: "Purple",
               budget: "High"
            );

            _collectionRepositoryMock
                .Setup(repo => repo.GetById(deleteCollectionRequest.Id))
                .ReturnsAsync(collection);

            _collectionRepositoryMock
                .Setup(repo => repo.Delete(collection))
                .Verifiable();

            _mapperMock
                .Setup(m => m.Map<CollectionResponse>(It.IsAny<Collection>()))
                .Returns((Collection c) => new CollectionResponse
                {
                    Budget = c.Budget,
                    Color = c.Color,
                    Id = c.Id,
                    Guns = new List<GunResponse>()
                });

            var collectionDeleteService = new DeleteCollectionHandler(_unitOfWorkMock.Object, _collectionRepositoryMock.Object, _mapperMock.Object);
            var cancellationToken = new CancellationToken();

            // Act
            var response = await collectionDeleteService.Handle(deleteCollectionRequest, cancellationToken);

            // Assert
            response.ShouldNotBeNull();
            response.Budget.ShouldBe(collection.Budget);
            response.Color.ShouldBe(collection.Color);

            _collectionRepositoryMock.Verify(repo => repo.Delete(collection), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async Task CollectionDoesNotExist_DeleteIsCalled_ReturnDefault()
        {
            // Arrange
            var deleteCollectionRequest = new Fixture().Create<DeleteCollectionRequest>();

            var cancellationToken = new CancellationToken();

            _collectionRepositoryMock
                .Setup(repo => repo.Get(deleteCollectionRequest.Id))
                .ReturnsAsync((Collection)null);

            var collectionDeleteService = new DeleteCollectionHandler(_unitOfWorkMock.Object, _collectionRepositoryMock.Object, _mapperMock.Object);

            // Act
            var response = await collectionDeleteService.Handle(deleteCollectionRequest, cancellationToken);

            // Assert
            response.ShouldBeNull();

            _collectionRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Collection>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }
    }
}
