using _0_Framework.Application;
using FleetManagement.Application.Contracts.AircraftType;
using FleetManagement.Application.Contracts.AircraftType.Commands;
using FleetManagement.Application.Contracts.AircraftType.DTOs;
using FleetManagement.Application.Contracts.AircraftType.Queries;
using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;

namespace FleetManagement.Application.Basic_data
{
    public class AircraftTypeApplication : IAircraftTypeApplication
    {
        private readonly IAircraftTypeRepository _repository;

        public AircraftTypeApplication(IAircraftTypeRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateAircraftTypeCommand command)
        {
            if (_repository.ExistsCode(command.Code))
                return new OperationResult().Failed("کد تایپ هواپیما تکراری است");

            var entity = AircraftType.Create(command.Code, command.Name, command.Description);
            _repository.Create(entity);
            _repository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult Edit(EditAircraftTypeCommand command)
        {
            var entity = _repository.Get(command.Id);
            if (entity == null)
                return new OperationResult().Failed("تایپ هواپیما یافت نشد");

            entity.Edit(command.Name, command.Description);
            _repository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult Activate(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return new OperationResult().Failed("تایپ هواپیما یافت نشد");

            entity.Activate();
            _repository.SaveChanges();
            return new OperationResult().Succeeded();
        }

        public OperationResult Deactivate(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return new OperationResult().Failed("تایپ هواپیما یافت نشد");

            entity.Deactivate();
            _repository.SaveChanges();
            return new OperationResult().Succeeded();
        }

        public AircraftTypeDto GetById(long id)
        {
            var entity = _repository.Get(id);
            if (entity == null) return null;

            return new AircraftTypeDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive
            };
        }

        public List<AircraftTypeDto> Search(SearchAircraftTypeQuery query)
        {
            var list = _repository.Search(query.Code, query.Name, query.IsActive);
            return list.Select(x => new AircraftTypeDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            }).ToList();
        }

        public List<AircraftTypeDto> GetAllActive()
        {
            var list = _repository.Search(null, null, true);
            return list.Select(x => new AircraftTypeDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
