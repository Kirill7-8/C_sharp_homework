using System.Collections.Generic;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Application.Discs
{
    public sealed class GetAllDiscsHandler
    {
        private readonly IDiscRepository _discRepository;

        public GetAllDiscsHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public List<Disc> Handle(GetAllDiscsQuery query)
        {
            return _discRepository.GetAll();
        }
    }
}