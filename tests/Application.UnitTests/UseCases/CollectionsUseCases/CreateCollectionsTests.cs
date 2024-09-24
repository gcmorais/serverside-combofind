using AutoFixture;
using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.CollectionUseCases.Create;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using Moq;
using Shouldly;

namespace Application.UnitTests.UseCases.CollectionsUseCases
{
    public class CreateCollectionsTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICollectionRepository> _collectionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IGunsRepository> _gunsRepository;

        public CreateCollectionsTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gunsRepository = new Mock<IGunsRepository>();
            _mapperMock = new Mock<IMapper>();
            _collectionRepositoryMock = new Mock<ICollectionRepository>();
        }

        [Fact]
        public async Task ValidCollection_CreateIsCalled_ReturnValidResponseCollection()
        {
            // Arrange
            var createCollectionRequest = new Fixture().Create<CreateCollectionRequest>();

            var collection = new Collection(
                color: createCollectionRequest.Color,
                budget: createCollectionRequest.Budget
            );

            _mapperMock.Setup(m => m.Map<Collection>(createCollectionRequest)).Returns(collection);
            _mapperMock.Setup(m => m.Map<CollectionResponse>(It.IsAny<Collection>())).Returns(new CollectionResponse
            {
                Budget = createCollectionRequest.Budget,
                Color = createCollectionRequest.Color,
                Id = Guid.NewGuid(),
                Guns = []
            });

            _collectionRepositoryMock.Setup(repo => repo.Create(It.IsAny<Collection>())).Verifiable();

            var collectionCreateService = new CreateCollectionHandler(_collectionRepositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
            var cancellationToken = new CancellationToken();

            // Act

            var response = await collectionCreateService.Handle(createCollectionRequest, cancellationToken);

            // Assert

            response.Color.ShouldBe(createCollectionRequest.Color);
            response.Budget.ShouldBe(createCollectionRequest.Budget);

            _collectionRepositoryMock.Verify(er => er.Create(It.Is<Collection>(u => u.Budget == createCollectionRequest.Budget)), Times.Once);

            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
