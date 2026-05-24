
using System.Text.Json.Serialization;

namespace MusicCatalog.Domain.Songs
{
    public sealed class Song
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public int Durration { get; private set; }
        public int Year { get; private set; }

        [JsonConstructor]
        private Song(Guid id, string title, string artist, int durration, int year)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Durration = durration;
            Year = year;

        }

        public static Song Create(string title, string artist, int durration, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название песни не может быть пустым");
            if (string.IsNullOrWhiteSpace(artist))
                throw new ArgumentException("Исполнитель не может быть пустым");
            if (durration <0)
                throw new ArgumentException("песня не может длиться меньше 0 сек");
            if (year < 1960 || year > 2026)
                throw new ArgumentException("год некорректный");
            return new Song(Guid.NewGuid(), title, artist, durration, year);
        }

        public void Update(string title, string artist, int durration, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название песни не может быть пустым");
            if (string.IsNullOrWhiteSpace(artist))
                throw new ArgumentException("Исполнитель не может быть пустым");
            if (durration < 0)
                throw new ArgumentException("песня не может длиться меньше 0 сек");
            if (year < 1960 || year > 2026)
                throw new ArgumentException("год некорректный");
            Title = title;
            Artist = artist;
            Durration = durration;
            Year = year;
        }
    }
}