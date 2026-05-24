using System;
using MusicCatalog.Application.Abstractions;

namespace MusicCatalog.Application.Songs
{
    public sealed class DeleteSongHandler
    {
        private readonly IDiscRepository _discRepository;

        public DeleteSongHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public void Handle(DeleteSongCommand command)
        {
            var disc = _discRepository.GetById(command.DiscId);
            if (disc == null)
                throw new InvalidOperationException("Диск не найден");

            disc.RemoveSong(command.SongId);
            _discRepository.Update(disc);
        }
    }
}