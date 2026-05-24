using System;
using System.Collections.Generic;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Application.Discs.Abstractions
{
    public interface IDiscsRepository
    {
        void Add(Disc disc);
        void Delete(Guid id);
        Disc? GetById(Guid id);
        IReadOnlyList<Disc> GetAll();
    }
}
