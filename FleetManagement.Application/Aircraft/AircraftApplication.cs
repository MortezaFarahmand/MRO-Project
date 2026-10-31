using _0_Framework.Application;
using _0_Framework.Domain;
using _01_MROQuery.Contracts.Organization;
using FleetManagement.Application.Contracts.Aircraft;
using FleetManagement.Application.Contracts.Aircraft.Commands;
using FleetManagement.Application.Contracts.Aircraft.DTOs;
using FleetManagement.Application.Contracts.Aircraft.Queries;
using FleetManagement.Domain.AircraftAgg;

namespace FleetManagement.Application
{
    public class AircraftApplication : IAircraftApplication
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAuthHelper _authHelper;
        private readonly IOrganizationQuery _organizationQuery;   // اختیاری – برای نام سازمان

        public AircraftApplication(
            IAircraftRepository aircraftRepository,
            IDomainEventDispatcher dispatcher,
            IAuthHelper authHelper,
            IOrganizationQuery organizationQuery = null)
        {
            _aircraftRepository = aircraftRepository;
            _dispatcher = dispatcher;
            _authHelper = authHelper;
            _organizationQuery = organizationQuery;
        }

        // ==================== Commands ====================

        public OperationResult Create(CreateAircraftCommand command)
        {
            if (_aircraftRepository.ExistsRegistration(command.Registration))
                return new OperationResult().Failed("رجیستری هواپیما تکراری است");

            if (_aircraftRepository.ExistsSerialNumber(command.SerialNumber))
                return new OperationResult().Failed("شماره سریال (MSN) هواپیما تکراری است");

            var aircraft = Aircraft.Create(
                command.Registration,
                command.SerialNumber,
                command.AircraftModelId,
                command.OwnerOrganizationId,
                command.OperatorOrganizationId,
                command.ManufactureDate,
                command.DeliveryDate,
                _authHelper.CurrentAccountId());

            _aircraftRepository.Create(aircraft);
            _aircraftRepository.SaveChanges();

            aircraft.RaiseCreatedEvent();
            _dispatcher.DispatchEvents(aircraft.DomainEvents);
            aircraft.ClearDomainEvents();

            return new OperationResult().Succeeded("هواپیما با موفقیت ثبت شد");
        }

        public OperationResult Edit(EditAircraftCommand command)
        {
            var aircraft = _aircraftRepository.Get(command.Id);
            if (aircraft == null)
                return new OperationResult().Failed("هواپیما یافت نشد");

            // بررسی یکتا بودن رجیستری (به جز خودش)
            var existing = _aircraftRepository.GetByRegistration(command.Registration);
            if (existing != null && existing.Id != command.Id)
                return new OperationResult().Failed("رجیستری هواپیما تکراری است");

            aircraft.Edit(
                command.Registration,
                command.OwnerOrganizationId,
                command.OperatorOrganizationId,
                command.ManufactureDate,
                command.DeliveryDate,
                command.RegistrationDate,
                command.LineNumber,
                command.VariableNumber,
                command.Note,
                _authHelper.CurrentAccountId());

            _aircraftRepository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult ChangeStatus(ChangeAircraftStatusCommand command)
        {
            var aircraft = _aircraftRepository.Get(command.Id);
            if (aircraft == null)
                return new OperationResult().Failed("هواپیما یافت نشد");

            aircraft.ChangeStatus(command.Status, _authHelper.CurrentAccountId());

            _aircraftRepository.SaveChanges();

            _dispatcher.DispatchEvents(aircraft.DomainEvents);
            aircraft.ClearDomainEvents();

            return new OperationResult().Succeeded("وضعیت هواپیما تغییر کرد");
        }

        public OperationResult UpdateLife(UpdateAircraftLifeCommand command)
        {
            var aircraft = _aircraftRepository.Get(command.Id);
            if (aircraft == null)
                return new OperationResult().Failed("هواپیما یافت نشد");

            aircraft.UpdateLifeValues(
                command.TotalFlightHours,
                command.TotalCycles,
                command.TotalLandings,
                _authHelper.CurrentAccountId());

            _aircraftRepository.SaveChanges();

            return new OperationResult().Succeeded("مقادیر Life هواپیما به‌روزرسانی شد");
        }

        public OperationResult Activate(long id)
        {
            var aircraft = _aircraftRepository.Get(id);
            if (aircraft == null)
                return new OperationResult().Failed("هواپیما یافت نشد");

            aircraft.Activate(_authHelper.CurrentAccountId());
            _aircraftRepository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        public OperationResult Deactivate(long id)
        {
            var aircraft = _aircraftRepository.Get(id);
            if (aircraft == null)
                return new OperationResult().Failed("هواپیما یافت نشد");

            aircraft.Deactivate(_authHelper.CurrentAccountId());
            _aircraftRepository.SaveChanges();

            return new OperationResult().Succeeded();
        }

        // ==================== Queries ====================

        public AircraftDto GetById(long id)
        {
            var aircraft = _aircraftRepository.Get(id);
            if (aircraft == null)
                return null;

            return MapToDto(aircraft);
        }

        public AircraftDto GetByRegistration(string registration)
        {
            var aircraft = _aircraftRepository.GetByRegistration(registration);
            if (aircraft == null)
                return null;

            return MapToDto(aircraft);
        }

        public List<AircraftDto> Search(SearchAircraftQuery query)
        {
            query ??= new SearchAircraftQuery();

            var aircrafts = _aircraftRepository.Search(
                query.Registration,
                query.SerialNumber,
                query.AircraftModelId,
                query.OrganizationId,
                query.Status);

            return aircrafts.Select(MapToDto).ToList();
        }

        // ==================== Private Mapper ====================

        private AircraftDto MapToDto(Aircraft aircraft)
        {
            string ownerName = null;
            string operatorName = null;

            if (_organizationQuery != null)
            {
                if (aircraft.OwnerOrganizationId.HasValue)
                    ownerName = _organizationQuery.GetNameEn(aircraft.OwnerOrganizationId.Value);

                if (aircraft.OperatorOrganizationId.HasValue)
                    operatorName = _organizationQuery.GetNameEn(aircraft.OperatorOrganizationId.Value);
            }

            return new AircraftDto
            {
                Id = aircraft.Id,
                Registration = aircraft.Registration,
                SerialNumber = aircraft.SerialNumber,
                AircraftModelId = aircraft.AircraftModelId,
                OwnerOrganizationId = aircraft.OwnerOrganizationId,
                OwnerOrganizationName = ownerName,
                OperatorOrganizationId = aircraft.OperatorOrganizationId,
                OperatorOrganizationName = operatorName,
                Status = aircraft.Status.ToString(),
                ManufactureDate = aircraft.ManufactureDate,
                DeliveryDate = aircraft.DeliveryDate,
                RegistrationDate = aircraft.RegistrationDate,
                TotalFlightHours = aircraft.TotalFlightHours,
                TotalCycles = aircraft.TotalCycles,
                TotalLandings = aircraft.TotalLandings,
                LineNumber = aircraft.LineNumber,
                VariableNumber = aircraft.VariableNumber,
                Note = aircraft.Note,
                IsActive = aircraft.IsActive
            };
        }
    }
}