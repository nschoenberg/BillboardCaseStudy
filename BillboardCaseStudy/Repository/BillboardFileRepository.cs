using BillboardCaseStudy.Contracts;
using BillboardCaseStudy.Contracts.DTO;
using BillboardCaseStudy.Contracts.Model;
using Newtonsoft.Json;

namespace BillboardCaseStudy.Repository
{
    public class BillboardFileRepository : IRepository
    {
        private readonly ILogger _logger;
        private readonly string _filePath;

        private readonly IReadOnlyList<Billboard> _emptyList = new List<Billboard>();
        private string? _data;

        public BillboardFileRepository(ILogger logger)
        {
            _logger = logger;
            _filePath = Path.Combine(Environment.CurrentDirectory, "TestData.json");
        }

        public async Task<IReadOnlyList<Billboard>> GetAllAsync()
        {
            try
            {
                // Instead of using static classes, abstract 3rd party into own tooling interface / class. System.IO namespace as well
                // Cache disk content in memory
                _data ??= await File.ReadAllTextAsync(_filePath);
                var result = JsonConvert.DeserializeObject<List<BillboardDto>>(_data);

                var models = result == null ? _emptyList : MapDtoCollectionToModel(result);
                return models;
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
                throw;
            }
        }

        private IReadOnlyList<Billboard> MapDtoCollectionToModel(IEnumerable<BillboardDto> billboardDtos)
        {
            return billboardDtos.Select(MapDtoToModel).ToList();
        }

        private Billboard MapDtoToModel(BillboardDto billboard)
        {
            // Map from Dto to internal Model. Idea is to not use data transport objects for internal logic, separation of concerns
            // For complexer scenarios it might be feasible to use something like Automapper
            
            return new Billboard
            {
                No = billboard.No,
                Price = billboard.Price,
                Latidude = billboard.Latidude,
                Longitude = billboard.Longitude,
            };
        }
    }
}
