using System;
using MusicCatalog.Application.Abstractions;

namespace MusicCatalog.Application.Songs
{
    public sealed class SortSongsByArtistHandler
    {
        private readonly IDiscRepository _discRepository;

        public SortSongsByArtistHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(SortSongsByArtistCommand command)
        {
            var disc = _discRepository.GetById(command.DiscId);
            if (disc == null)
                throw new InvalidOperationException("Диск не найден");

            disc.SortSongsByArtist();
            _discRepository.Update(disc);
        }
    }
}