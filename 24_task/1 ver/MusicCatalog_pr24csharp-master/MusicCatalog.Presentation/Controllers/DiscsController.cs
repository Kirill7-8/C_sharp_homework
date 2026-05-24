using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Abstractions;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Songs;
using MusicCatalog.Infrastructure.Discs;

namespace MusicCatalog.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscsController : ControllerBase
    {
        private static readonly DiscRepository _repository = new DiscRepository();
        private static readonly AddDiscHandler _addDiscHandler = new AddDiscHandler(_repository);
        private static readonly DeleteDiscHandler _deleteDiscHandler = new DeleteDiscHandler(_repository);
        private static readonly GetAllDiscsHandler _getAllDiscsHandler = new GetAllDiscsHandler(_repository);
        private static readonly GetDiscByIdHandler _getDiscByIdHandler = new GetDiscByIdHandler(_repository);
        private static readonly AddSongToDiscHandler _addSongHandler = new AddSongToDiscHandler(_repository);
        private static readonly DeleteSongHandler _deleteSongHandler = new DeleteSongHandler(_repository);
        private static readonly SearchSongsByArtistHandler _searchSongsHandler = new SearchSongsByArtistHandler(_repository);
        private static readonly SortSongsByNameHandler _sortByNameHandler = new SortSongsByNameHandler(_repository);
        private static readonly SortSongsByArtistHandler _sortByArtistHandler = new SortSongsByArtistHandler(_repository);

        [HttpPost]
        public IActionResult AddDisc([FromBody] AddDiscCommand command)
        {
            try
            {
                _addDiscHandler.Handle(command);
                return Ok(new { message = "Диск добавлен" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDisc(Guid id)
        {
            _deleteDiscHandler.Handle(new DeleteDiscCommand { Id = id });
            return Ok(new { message = "Диск удален" });
        }

        [HttpGet]
        public IActionResult GetAllDiscs()
        {
            var result = _getAllDiscsHandler.Handle(new GetAllDiscsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscById(Guid id)
        {
            var result = _getDiscByIdHandler.Handle(new GetDiscByIdQuery { Id = id });
            if (result == null)
                return NotFound(new { message = "Диск не найден" });
            return Ok(result);
        }

        [HttpPost("{discId}/songs")]
        public IActionResult AddSong(Guid discId, [FromBody] AddSongToDiscCommand command)
        {
            try
            {
                command.DiscId = discId;
                _addSongHandler.Handle(command);
                return Ok(new { message = "Песня добавлена" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{discId}/songs/{songId}")]
        public IActionResult DeleteSong(Guid discId, Guid songId)
        {
            try
            {
                _deleteSongHandler.Handle(new DeleteSongCommand { DiscId = discId, SongId = songId });
                return Ok(new { message = "Песня удалена" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchSongsByArtist([FromQuery] string artist)
        {
            var result = _searchSongsHandler.Handle(new SearchSongsByArtistQuery { Artist = artist });
            return Ok(result);
        }

        [HttpPost("{discId}/sort/by-name")]
        public IActionResult SortSongsByName(Guid discId)
        {
            try
            {
                _sortByNameHandler.Handle(new SortSongsByNameCommand { DiscId = discId });
                return Ok(new { message = "Песни отсортированы по названию" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{discId}/sort/by-artist")]
        public IActionResult SortSongsByArtist(Guid discId)
        {
            try
            {
                _sortByArtistHandler.Handle(new SortSongsByArtistCommand { DiscId = discId });
                return Ok(new { message = "Песни отсортированы по исполнителю" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}