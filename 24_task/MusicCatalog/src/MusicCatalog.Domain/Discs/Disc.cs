using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.Domain.Discs
{
    public sealed class Disc
    {
        private Disc(Guid id, string title, string artist, int year)
        {
            Id     = id;
            Title  = title;
            Artist = artist;
            Year   = year;
        }

        public Guid   Id     { get; private set; }
        public string Title  { get; private set; }
        public string Artist { get; private set; }
        public int    Year   { get; private set; }

        public static Disc Create(string title, string artist, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Название диска не может быть пустым");
            if (string.IsNullOrWhiteSpace(artist))
                throw new Exception("Исполнитель диска не может быть пустым");
            if (year < 1900 || year > DateTime.Today.Year + 1)
                throw new Exception($"Некорректный год выпуска: {year}");

            return new Disc(Guid.NewGuid(), title, artist, year);
        }
    }
}
