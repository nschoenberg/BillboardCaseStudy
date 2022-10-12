using System.Diagnostics;
using BillboardCaseStudy.Contracts.Model;
using BillboardCaseStudy.Logging;
using BillboardCaseStudy.Repository;

namespace BillboardCaseStudy.Tests
{
    public class BillboardSelectorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(100.20)]
        [InlineData(100.45)]
        [InlineData(100.80)]
        [InlineData(1000)]

        public async void Select_ForGivenBudget_DoesNotSlipLimits(decimal budget)
        {
            var repository = CreateRepository();
            var data = await repository.GetAllAsync();
            var selector = new BillboardSelector();
            
            var result = selector.Select(data, budget);

            var sum = result.Sum(board => board.Price);

            Assert.InRange(sum, 0, budget);
        }


        [Theory]
        [InlineData(100, 100.00)]
        [InlineData(1000, 999.95)]
        public async void Select_ForGivenBudget_ReturnsExpectedSum(decimal budget, decimal expected)
        {
            var repository = CreateRepository();
            var data = await repository.GetAllAsync();
            var selector = new BillboardSelector();

            var result = selector.Select(data, budget);
            var sum = result.Sum(board => board.Price);

            Assert.Equal(expected, sum);
        }


        [Fact]
        public async void Select_HasGoodEnoughPerformance_ForHigherDataLoad()
        {
            // Simple scale test that logic works 'good enough' given a data set that is 10 times larger compared to the expected test data
            
            var repository = CreateRepository();
            var data = await repository.GetAllAsync();

            var largeDataSet = new List<Billboard>();
            for (var i = 0; i < 9; i++)
            {
                largeDataSet.AddRange(data);
            }

            var selector = new BillboardSelector();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            _ = selector.Select(largeDataSet, 100000m);
            stopWatch.Stop();

            Assert.InRange(stopWatch.ElapsedMilliseconds, 0, 1500);
        }

        private static BillboardFileRepository CreateRepository()
        {
            return new BillboardFileRepository(new ConsoleLogger());
        }
    }
}
