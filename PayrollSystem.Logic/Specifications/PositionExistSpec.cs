using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Specifications.Base;

namespace PayrollSystem.Logic.Specifications
{
    internal class PositionExistSpec : Specification<Position>
    {
        public PositionExistSpec(int id, string name)
        {
            name = name?.Trim()?.ToUpper() ?? null;

            Query = e => e.Name.ToUpper() == name && (id == 0 || e.ID != id);
        }
    }
}
