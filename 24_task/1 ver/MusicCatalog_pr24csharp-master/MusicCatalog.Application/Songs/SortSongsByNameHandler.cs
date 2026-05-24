using System;
using MusicCatalog.Application.Abstractions;

namespace MusicCatalog.Application.Songs
{
    public sealed class SortSongsByNameHandler
    {
        private readonly IDiscRepository _discRepository;

        public SortSongsByNameHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(SortSongsByNameCommand command)
        {
            var disc = _discRepository.GetById(command.DiscId);
            if (disc == null)
                throw new InvalidOperationException("Диск не найден");

            disc.SortSongsByName();
            _discRepository.Update(disc);
        }
    }
}