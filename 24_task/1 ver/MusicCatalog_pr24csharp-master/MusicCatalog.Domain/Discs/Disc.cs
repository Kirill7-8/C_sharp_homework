using MusicCatalog.Domain.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MusicCatalog.Domain.Discs
{
    public sealed class Disc
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public List<Song> Songs { get; private set; }

        [JsonConstructor]
        private Disc(Guid id, string title)
        {
            Id = id;
            Title = title;
            Songs = new List<Song>();
        }

        public static Disc Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название диска не может быть пустым");
            return new Disc(Guid.NewGuid(), title);
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название диска не может быть пустым");
            Title = title;
        }

        public void AddSong(Song song)
        {
            if (Songs.Any(s => s.Title.Equals(song.Title, StringComparison.OrdinalIgnoreCase) && s.Artist.Equals(song.Artist, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Песня '{song.Title}' исполнителя '{song.Artist}' уже есть на диске");
            Songs.Add(song);
        }

        public void RemoveSong(Guid songId)
        {
            var song = Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null)
                throw new InvalidOperationException("Песня не найдена");
            Songs.Remove(song);
        }

        public void SortSongsByName()
        {
            Songs = Songs.OrderBy(s => s.Title, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public void SortSongsByArtist()
        {
            Songs = Songs.OrderBy(s => s.Artist, StringComparer.OrdinalIgnoreCase).ToList();
        }
    }
}