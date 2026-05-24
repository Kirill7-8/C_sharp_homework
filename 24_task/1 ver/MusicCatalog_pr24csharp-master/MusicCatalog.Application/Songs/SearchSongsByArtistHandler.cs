using System.Collections.Generic;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Application.Songs
{
    public sealed class SearchSongsByArtistHandler
    {
        private readonly IDiscRepository _discRepository;

        public SearchSongsByArtistHandler(IDiscRepository discRepository)
        {
            _discRepository = discRepository;
        }

        public List<Song> Handle(SearchSongsByArtistQuery query)
        {
            return _discRepository.GetAllSongsByArtist(query.Artist);
        }
    }
}