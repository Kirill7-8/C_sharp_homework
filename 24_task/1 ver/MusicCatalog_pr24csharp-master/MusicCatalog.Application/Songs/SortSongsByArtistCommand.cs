using System;

namespace MusicCatalog.Application.Songs
{
    public sealed class SortSongsByArtistCommand
    {
        public Guid DiscId { get; set; }
    }
}