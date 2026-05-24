using System;

namespace MusicCatalog.Application.Songs
{
    public sealed class AddSongToDiscCommand
    {
        public Guid DiscId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Durration { get; set; }
        public int Year { get; set; }
    }
}