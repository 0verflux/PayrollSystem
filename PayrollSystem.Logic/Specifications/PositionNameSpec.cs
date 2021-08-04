using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Specifications.Base;

namespace PayrollSystem.Logic.Specifications
{
    internal class PositionNameSpec : Specification<Position>
    {
        public PositionNameSpec(string search)
        {
            search = search?.Trim()?.ToUpper() ?? null;

            Query = e => search == null || e.Name.ToUpper().Contains(search);
        }
    }
}
