using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Application.Discs
{
    public sealed class GetDiscByIdHandler
    {
        private readonly IDiscRepository _discRepository;

        public GetDiscByIdHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public Disc Handle(GetDiscByIdQuery query)
        {
            return _discRepository.GetById(query.Id);
        }
    }
}