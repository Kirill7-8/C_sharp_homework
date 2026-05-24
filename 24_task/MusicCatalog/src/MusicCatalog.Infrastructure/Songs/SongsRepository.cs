using System;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Infrastructure.Songs
{
    public sealed class SongsRepository : ISongsRepository
    {
        private readonly Dictionary<Guid, Song> _songs = new();

        public void Add(Song song) => _songs[song.Id] = song;
        public void Delete(Guid id) => _songs.Remove(id);
        public Song? GetById(Guid id) => _songs.GetValueOrDefault(id);
        public IReadOnlyList<Song> GetAll() => _songs.Values.ToList();

        public IReadOnlyList<Song> GetByDiscId(Guid discId) =>
            _songs.Values.Where(s => s.DiscId == discId).ToList();

        public IReadOnlyList<Song> GetByArtist(string artist) =>
            _songs.Values
                  .Where(s => s.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase))
                  .ToList();
    }
}
