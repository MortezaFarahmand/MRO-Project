using _0_Framework.Application;
using _0_Framework.Domain;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Application.Part.CommandHandlers
{
    public class CreatePartCommandHandler
    {
        private readonly IPartRepository _partRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAuthHelper _authHelper; // یا ICurrentUser

        public CreatePartCommandHandler(
            IPartRepository partRepository,
            IDomainEventDispatcher dispatcher,
            IAuthHelper authHelper)
        {
            _partRepository = partRepository;
            _dispatcher = dispatcher;
            _authHelper = authHelper;
        }

        public OperationResult Handle(CreatePartCommand command)
        {
            if (_partRepository.Exists(command.PartNumber))
                return new OperationResult().Failed("شماره قطعه تکراری است");

            var currentUserId = _authHelper.CurrentAccountId(); // مطابق کد شما

            var part = PartManagement.Domain.PartAgg.Part.Create(
                command.PartNumber,
                command.Description,
                command.ControlType,
                command.IsGlobal,
                command.OwnerOrganizationId,
                currentUserId);

            // مقداردهی فیلدهای اضافی (اگر متدهای مربوطه را در Domain نوشته‌اید)
            // part.SetAdditionalInfo(...);

            _partRepository.Create(part);
            _partRepository.SaveChanges();

            part.RaiseCreatedEvent();
            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded("قطعه با موفقیت ثبت شد");
        }
    }
}