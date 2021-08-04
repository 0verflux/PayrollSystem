using PayrollSystem.Logic.Domain.Positions.DTOs;
using System.Collections.Generic;

namespace PayrollSystem.Logic.Contracts
{
    public interface IPositionManager
    {
        void CreatePosition(PositionDTO positionDTO);
        void UpdatePosition(PositionDTO positionDTO);
        void DeletePosition(PositionDTO positionDTO);
        IEnumerable<PositionDTO> GetPositions(string search = null);
        IEnumerable<PositionDTO> GetAllPositions();
        bool PositionExists(int id, string name);
        IEnumerable<PositionMiniDTO> GetAllPositionsWithActiveEmployees();
    }
}
