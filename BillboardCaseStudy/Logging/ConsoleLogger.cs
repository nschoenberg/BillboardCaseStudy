using BillboardCaseStudy.Contracts;

namespace BillboardCaseStudy.Logging
{
    /// <summary>
    /// Logging class to showcase DI principle
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
