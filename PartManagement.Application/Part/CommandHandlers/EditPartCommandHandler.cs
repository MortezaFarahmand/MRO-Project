using _0_Framework.Application;
using _0_Framework.Domain;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Application.Part.CommandHandlers
{
    public class EditPartCommandHandler
    {
        private readonly IPartRepository _partRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAuthHelper _authHelper;

        public EditPartCommandHandler(
            IPartRepository partRepository,
            IDomainEventDispatcher dispatcher,
            IAuthHelper authHelper)
        {
            _partRepository = partRepository;
            _dispatcher = dispatcher;
            _authHelper = authHelper;
        }

        public OperationResult Handle(EditPartCommand command)
        {
            var part = _partRepository.Get(command.Id);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.Edit(
                command.Description,
                command.Keyword,
                command.Specification,
                command.Note,
                _authHelper.CurrentAccountId());

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded();
        }
    }
}
