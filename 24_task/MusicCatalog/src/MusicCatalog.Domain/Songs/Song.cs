using System;

namespace MusicCatalog.Domain.Songs
{
    public sealed class Song
    {
        private Song(Guid id, Guid discId, string title, string artist, int durationSeconds, int trackNumber)
        {
            Id              = id;
            DiscId          = discId;
            Title           = title;
            Artist          = artist;
            DurationSeconds = durationSeconds;
            TrackNumber     = trackNumber;
        }

        public Guid   Id              { get; private set; }
        public Guid   DiscId          { get; private set; }
        public string Title           { get; private set; }
        public string Artist          { get; private set; }
        public int    DurationSeconds { get; private set; }
        public int    TrackNumber     { get; private set; }

        public static Song Create(Guid discId, string title, string artist, int durationSeconds, int trackNumber)
        {
            if (discId == Guid.Empty)
                throw new Exception("Не указан диск для песни");
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Название песни не может быть пустым");
            if (string.IsNullOrWhiteSpace(artist))
                throw new Exception("Исполнитель песни не может быть пустым");
            if (durationSeconds <= 0)
                throw new Exception("Длительность песни должна быть больше нуля");
            if (trackNumber <= 0)
                throw new Exception("Номер трека должен быть больше нуля");

            return new Song(Guid.NewGuid(), discId, title, artist, durationSeconds, trackNumber);
        }
    }
}
