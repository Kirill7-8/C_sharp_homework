using System;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Tests.Fakes
{
    public class FakeDiscRepository : IDiscRepository
    {
        private readonly List<Disc> _discs = new();

        public void Add(Disc disc)
        {
            _discs.Add(disc);
        }

        public void Delete(Guid id)
        {
            _discs.RemoveAll(d => d.Id == id);
        }

        public Disc GetById(Guid id)
        {
            return _discs.FirstOrDefault(d => d.Id == id);
        }

        public List<Disc> GetAll()
        {
            return _discs.ToList();
        }

        public void Update(Disc disc)
        {
            var index = _discs.FindIndex(d => d.Id == disc.Id);
            if (index != -1)
            {
                _discs[index] = disc;
            }
        }

        public Disc GetByTitle(string title)
        {
            return _discs.FirstOrDefault(d =>
                d.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Song> GetAllSongsByArtist(string artist)
        {
            return _discs
                .SelectMany(d => d.Songs)
                .Where(s => s.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}