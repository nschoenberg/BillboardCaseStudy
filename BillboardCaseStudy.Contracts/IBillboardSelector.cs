using BillboardCaseStudy.Contracts.Model;

namespace BillboardCaseStudy.Contracts
{
    public interface IBillboardSelector
    {
        IReadOnlyList<Billboard> Select(IReadOnlyList<Billboard> billboards, decimal budget);
    }
}