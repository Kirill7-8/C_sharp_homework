using System;
using System.Collections.Generic;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Application.Discs
{
    public sealed class DiscsService
    {
        private readonly IDiscsRepository _discsRepo;
        private readonly ISongsRepository _songsRepo;

        public DiscsService(IDiscsRepository discsRepo, ISongsRepository songsRepo)
        {
            _discsRepo = discsRepo;
            _songsRepo = songsRepo;
        }

        public Guid CreateDisc(string title, string artist, int year)
        {
            var disc = Disc.Create(title, artist, year);
            _discsRepo.Add(disc);
            return disc.Id;
        }

        public void DeleteDisc(Guid id)
        {
            var disc = _discsRepo.GetById(id);
            if (disc == null)
                throw new Exception($"Диск с id={id} не найден");

            var songs = _songsRepo.GetByDiscId(id);
            foreach (var song in songs)
                _songsRepo.Delete(song.Id);

            _discsRepo.Delete(id);
        }

        public Disc GetDisc(Guid id)
        {
            var disc = _discsRepo.GetById(id);
            if (disc == null)
                throw new Exception($"Диск с id={id} не найден");
            return disc;
        }

        public IReadOnlyList<Disc> GetAllDiscs() => _discsRepo.GetAll();
    }
}
