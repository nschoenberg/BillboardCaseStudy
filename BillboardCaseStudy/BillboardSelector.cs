using BillboardCaseStudy.Contracts;
using BillboardCaseStudy.Contracts.Model;

namespace BillboardCaseStudy
{
    public class BillboardSelector : IBillboardSelector
    {
        public IReadOnlyList<Billboard> Select(IReadOnlyList<Billboard> billboards, decimal budget)
        {
            var availableBoards = billboards.OrderByDescending(e => e.Price).ToList();
            return SelectInternal(availableBoards, budget);
        }

        private static IReadOnlyList<Billboard> SelectInternal(List<Billboard> billboards, decimal budget)
        {
            var availableBoards = billboards.Where(e => e.Price <= budget).ToList();

            if (availableBoards.Count == 0)
            {
                return new List<Billboard>();
            }

            var board = availableBoards.First();
            availableBoards.Remove(board);
            var restBudget = budget - board.Price;

            var result = new List<Billboard> { board };

            if (restBudget >= availableBoards.Last().Price)
            {
                result.AddRange(SelectInternal(availableBoards, restBudget));
            }

            return result;
        }
    }
}