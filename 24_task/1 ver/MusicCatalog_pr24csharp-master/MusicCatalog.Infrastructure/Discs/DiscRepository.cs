using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Infrastructure.Discs
{
    public sealed class DiscRepository : IDiscRepository
    {
        private readonly string _filePath = "discs.json";
        private List<Disc> _discs;

        public DiscRepository()
        {
            Load();
        }

        private void Load()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _discs = JsonSerializer.Deserialize<List<Disc>>(json) ?? new List<Disc>();
            }
            else
            {
                _discs = new List<Disc>();
            }
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(_discs);
            File.WriteAllText(_filePath, json);
        }

        public void Add(Disc disc)
        {
            _discs.Add(disc);
            Save();
        }

        public void Delete(Guid id)
        {
            _discs.RemoveAll(d => d.Id == id);
            Save();
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
                Save();
            }
        }

        public List<Song> GetAllSongsByArtist(string artist)
        {
            return _discs
                .SelectMany(d => d.Songs)
                .Where(s => s.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Disc GetByTitle(string title) //метод для проверки на дубликаты
        {
            return _discs.FirstOrDefault(d => d.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}