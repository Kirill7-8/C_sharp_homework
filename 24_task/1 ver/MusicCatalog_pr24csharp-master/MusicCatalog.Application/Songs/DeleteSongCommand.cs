using System;

namespace MusicCatalog.Application.Songs
{
    public sealed class DeleteSongCommand
    {
        public Guid DiscId { get; set; }
        public Guid SongId { get; set; }
    }
}