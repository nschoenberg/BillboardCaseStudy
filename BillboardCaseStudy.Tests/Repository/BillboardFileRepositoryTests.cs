using BillboardCaseStudy.Logging;
using BillboardCaseStudy.Repository;

namespace BillboardCaseStudy.Tests.Repository
{
    public class BillboardFileRepositoryTests
    {
        [Fact]
        public async void FileRepository_GetAllAsync_IsNotEmpty()
        {
            // Arange
            var repository = CreateRepository();

            // Act
            var data = await repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(data);
        }

        [Fact]
        public async void FileRepository_GetAllAsync_ContainsExpectedAmountOfElements()
        {
            var repository = CreateRepository();

            var data = await repository.GetAllAsync();

            Assert.Equal(2495, data.Count);
        }

        private static BillboardFileRepository CreateRepository()
        {
            return new BillboardFileRepository(new ConsoleLogger());
        }

    }
}
