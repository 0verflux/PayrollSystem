using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Logic.Specifications
{
    internal class PositionIDSpec : Specification<Position>
    {
        public PositionIDSpec(int id)
        {
            Query = e => e.ID == id;
        }
    }
}
