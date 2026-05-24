using System;
using System.Collections.Generic;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Application.Songs.Abstractions
{
    public interface ISongsRepository
    {
        void Add(Song song);
        void Delete(Guid id);
        Song? GetById(Guid id);
        IReadOnlyList<Song> GetAll();
        IReadOnlyList<Song> GetByDiscId(Guid discId);
        IReadOnlyList<Song> GetByArtist(string artist);
    }
}
