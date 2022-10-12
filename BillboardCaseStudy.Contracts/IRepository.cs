using BillboardCaseStudy.Contracts.Model;

namespace BillboardCaseStudy.Contracts
{
    public interface IRepository
    {
        Task<IReadOnlyList<Billboard>> GetAllAsync();
    }
}