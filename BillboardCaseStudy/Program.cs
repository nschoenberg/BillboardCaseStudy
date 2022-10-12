using BillboardCaseStudy.Contracts;
using BillboardCaseStudy.Contracts.Model;
using BillboardCaseStudy.Logging;
using BillboardCaseStudy.Repository;
using ConsoleTables;
using SimpleInjector;

namespace BillboardCaseStudy 
{
    internal class Program
    {
        private static Container _container;

        static Program()
        {
            _container = new Container();

            _container.Register<IBillboardSelector, BillboardSelector>(Lifestyle.Singleton);
            _container.Register<IRepository, BillboardFileRepository>(Lifestyle.Singleton);
            _container.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);
            
            _container.Verify();
        }

        static async Task Main(string[] args)
        {
            var maxBudget = 1000m;
            
            var repository = _container.GetInstance<IRepository>();
            var selector = _container.GetInstance<IBillboardSelector>();

            var data = await repository.GetAllAsync();
            var result = selector.Select(data, maxBudget);
            var sum = result.Sum(board => board.Price);

            ConsoleTable
                .From(result)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);

            Console.WriteLine($"For budget of '{maxBudget}' the table shows one possible solution. {result.Count} different boards having a total price sum of '{sum}'.");

            Console.ReadKey();

        }
    }
}