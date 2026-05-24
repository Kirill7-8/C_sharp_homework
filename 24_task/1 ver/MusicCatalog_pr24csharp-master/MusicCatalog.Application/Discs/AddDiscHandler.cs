using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Application.Discs
{
    public sealed class AddDiscHandler
    {
        private readonly IDiscRepository _discRepository;

        public AddDiscHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(AddDiscCommand command)
        {
            //проверка на дубликаты
            var existingDisc = _discRepository.GetByTitle(command.Title);
            if (existingDisc != null)
            {
                throw new InvalidOperationException($"Диск с названием '{command.Title}' уже существует");
            }

            var disc = Disc.Create(command.Title);
            _discRepository.Add(disc);
        }
    }
}