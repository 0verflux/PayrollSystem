using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Logic.Contexts;
using PayrollSystem.Logic.Contracts;
using PayrollSystem.Logic.Domain.Positions;
using PayrollSystem.Logic.Domain.Positions.DTOs;
using PayrollSystem.Logic.Exceptions;
using PayrollSystem.Logic.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PayrollSystem.Logic.Application
{
    public class PositionManager : IPositionManager
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<PayrollDBContext> contextFactory;

        public PositionManager(IMapper mapper, IDbContextFactory<PayrollDBContext> contextFactory)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public void CreatePosition(PositionDTO positionDTO)
        {
            using var context = contextFactory.CreateDbContext();

            var position = mapper.Map<Position>(positionDTO);

            if (Exists(context, default, position.Name))
                throw new EntityAlreadyExistsException("Position Name");

            context.Positions.Add(position);
            context.SaveChanges();
        }

        public void UpdatePosition(PositionDTO positionDTO)
        {
            using var context = contextFactory.CreateDbContext();

            var position = mapper.Map<Position>(positionDTO);

            if (Exists(context, position.ID, position.Name))
                throw new EntityAlreadyExistsException("Position Name");

            context.Positions.Update(position);
            context.SaveChanges();
        }

        public void DeletePosition(PositionDTO positionDTO)
        {
            using var context = contextFactory.CreateDbContext();
            
            var position = mapper.Map<Position>(positionDTO);

            context.Positions.Remove(position);
            context.SaveChanges();
        }

        public IEnumerable<PositionDTO> GetAllPositions()
        {
            using var context = contextFactory.CreateDbContext();

            var result = context
                .Positions
                .ToList();

            return mapper.Map<List<Position>, List<PositionDTO>>(result);
        }

        public IEnumerable<PositionMiniDTO> GetAllPositionsWithActiveEmployees()
        {
            using var context = contextFactory.CreateDbContext();

            var result = context
                .Positions
                .Include(e => e.Employees.Where(e => e.EmploymentDate.End == null || e.EmploymentDate.End.Value >= DateTime.Now))
                .ThenInclude(e => e.Position)
                .ToList();

            return mapper.Map<List<Position>, List<PositionMiniDTO>>(result);
        }

        public bool PositionExists(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            using var context = contextFactory.CreateDbContext();
            return Exists(context, id, name);
        }

        public IEnumerable<PositionDTO> GetPositions(string search = null)
        {
            using var context = contextFactory.CreateDbContext();

            var nameSpec = new PositionNameSpec(search);

            var result = context
                .Positions
                .Where(nameSpec)
                .ToList();

            return mapper.Map<List<Position>, List<PositionDTO>>(result);
        }

        private static bool Exists(PayrollDBContext context, int id, string name)
        {
            var positionExistSpec = new PositionExistSpec(id, name);

            var result = context
                .Positions
                .Any(positionExistSpec);

            return result;
        }
    }
}
