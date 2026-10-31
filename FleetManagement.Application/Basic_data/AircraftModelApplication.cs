using _0_Framework.Application;
using FleetManagement.Application.Contracts.AircraftModel;
using FleetManagement.Application.Contracts.AircraftModel.Commands;
using FleetManagement.Application.Contracts.AircraftModel.DTOs;
using FleetManagement.Application.Contracts.AircraftModel.Queries;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;

namespace FleetManagement.Application
{
    public class AircraftModelApplication : IAircraftModelApplication
    {
        private readonly IAircraftModelRepository _repository;

        public AircraftModelApplication(IAircraftModelRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateAircraftModelCommand command)
        {
            if (_repository.ExistsCode(command.Code))
                return new OperationResult().Failed("کد مدل هواپیما تکراری است");

            var entity = AircraftModel.Create(
                command.Code,
                command.Name,
                command.AircraftTypeId,
                command.Manufacturer,
                command.Description);

            _repository.Create(entity);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("مدل هواپیما با موفقیت ثبت شد");
        }

        public OperationResult Edit(EditAircraftModelCommand command)
        {
            var entity = _repository.Get(command.Id);
            if (entity == null)
                return new OperationResult().Failed("مدل هواپیما یافت نشد");

            entity.Edit(command.Name, command.Manufacturer, command.Description);
            _repository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult Activate(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return new OperationResult().Failed("مدل هواپیما یافت نشد");

            entity.Activate();
            _repository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult Deactivate(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return new OperationResult().Failed("مدل هواپیما یافت نشد");

            entity.Deactivate();
            _repository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public AircraftModelDto GetById(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return null;

            return MapToDto(entity);
        }

        public List<AircraftModelDto> GetByTypeId(long aircraftTypeId)
        {
            var list = _repository.GetByTypeId(aircraftTypeId);
            return list.Select(MapToDto).ToList();
        }

        public List<AircraftModelDto> Search(SearchAircraftModelQuery query)
        {
            query ??= new SearchAircraftModelQuery();

            var list = _repository.Search(
                query.Code,
                query.Name,
                query.AircraftTypeId,
                query.Manufacturer,
                query.IsActive);

            return list.Select(MapToDto).ToList();
        }

        private AircraftModelDto MapToDto(AircraftModel entity)
        {
            return new AircraftModelDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                AircraftTypeId = entity.AircraftTypeId,
                Manufacturer = entity.Manufacturer,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }
    }
}