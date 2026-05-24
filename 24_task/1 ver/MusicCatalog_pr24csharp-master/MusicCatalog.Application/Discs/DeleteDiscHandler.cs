using MusicCatalog.Application.Abstractions;

namespace MusicCatalog.Application.Discs
{
    public sealed class DeleteDiscHandler
    {
        private readonly IDiscRepository _discRepository;

        public DeleteDiscHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(DeleteDiscCommand command)
        {
            _discRepository.Delete(command.Id);
        }
    }
}