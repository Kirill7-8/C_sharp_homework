using System;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Application.Songs
{
    public sealed class SongsService
    {
        private readonly ISongsRepository _songsRepo;
        private readonly IDiscsRepository _discsRepo;

        public SongsService(ISongsRepository songsRepo, IDiscsRepository discsRepo)
        {
            _songsRepo = songsRepo;
            _discsRepo = discsRepo;
        }

        public Guid AddSong(Guid discId, string title, string artist, int durationSeconds, int trackNumber)
        {
            var disc = _discsRepo.GetById(discId);
            if (disc == null)
                throw new Exception($"Диск с id={discId} не найден");

            var song = Song.Create(discId, title, artist, durationSeconds, trackNumber);
            _songsRepo.Add(song);
            return song.Id;
        }

        public void DeleteSong(Guid id)
        {
            var song = _songsRepo.GetById(id);
            if (song == null)
                throw new Exception($"Песня с id={id} не найдена");
            _songsRepo.Delete(id);
        }

        public IReadOnlyList<Song> GetSongsByDisc(Guid discId) =>
            _songsRepo.GetByDiscId(discId);

        public IReadOnlyList<Song> GetAllSongs() =>
            _songsRepo.GetAll();

        public IReadOnlyList<Song> SearchByArtist(string artist) =>
            _songsRepo.GetByArtist(artist);

        public IReadOnlyList<Song> GetSortedByTitle(Guid discId) =>
            _songsRepo.GetByDiscId(discId)
                      .OrderBy(s => s.Title, StringComparer.OrdinalIgnoreCase)
                      .ToList();

        public IReadOnlyList<Song> GetSortedByArtist(Guid discId) =>
            _songsRepo.GetByDiscId(discId)
                      .OrderBy(s => s.Artist, StringComparer.OrdinalIgnoreCase)
                      .ToList();
    }
}
