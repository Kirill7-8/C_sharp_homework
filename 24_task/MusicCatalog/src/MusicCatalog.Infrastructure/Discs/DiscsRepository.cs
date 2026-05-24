using System;
using System.Collections.Generic;
using System.Linq;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Infrastructure.Discs
{
    public sealed class DiscsRepository : IDiscsRepository
    {
        private readonly Dictionary<Guid, Disc> _discs = new();

        public void Add(Disc disc) => _discs[disc.Id] = disc;
        public void Delete(Guid id) => _discs.Remove(id);
        public Disc? GetById(Guid id) => _discs.GetValueOrDefault(id);
        public IReadOnlyList<Disc> GetAll() => _discs.Values.ToList();
    }
}
