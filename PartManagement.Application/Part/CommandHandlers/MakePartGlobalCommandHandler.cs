using _0_Framework.Application;
using _0_Framework.Domain;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Application.Part.CommandHandlers
{
    public class MakePartGlobalCommandHandler
    {
        private readonly IPartRepository _partRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAuthHelper _authHelper;

        public MakePartGlobalCommandHandler(
            IPartRepository partRepository,
            IDomainEventDispatcher dispatcher,
            IAuthHelper authHelper)
        {
            _partRepository = partRepository;
            _dispatcher = dispatcher;
            _authHelper = authHelper;
        }

        public OperationResult Handle(MakePartGlobalCommand command)
        {
            var part = _partRepository.Get(command.Id);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.MakeGlobal(_authHelper.CurrentAccountId());

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded("قطعه به حالت سراسری تغییر یافت");
        }
    }
}
