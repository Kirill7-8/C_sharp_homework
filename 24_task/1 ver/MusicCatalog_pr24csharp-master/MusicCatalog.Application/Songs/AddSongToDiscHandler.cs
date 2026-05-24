using System;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Application.Songs
{
    public sealed class AddSongToDiscHandler
    {
        private readonly IDiscRepository _discRepository;

        public AddSongToDiscHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(AddSongToDiscCommand command)
        {
            var disc = _discRepository.GetById(command.DiscId);
            if (disc == null)
                throw new InvalidOperationException("Диск не найден");

            var song = Song.Create(command.Title, command.Artist, command.Durration, command.Year);
            disc.AddSong(song);
            _discRepository.Update(disc);
        }
    }
}