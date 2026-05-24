using System;

namespace MusicCatalog.Application.Songs
{
    public sealed class SortSongsByNameCommand
    {
        public Guid DiscId { get; set; }
    }
}