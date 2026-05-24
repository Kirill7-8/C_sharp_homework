using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Songs;
using MusicCatalog.Domain.Songs;
using MusicCatalog.Presentation.Contracts;

namespace MusicCatalog.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongsService _service;

        public SongsController(SongsService service) => _service = service;

        /// <summary>Получить все песни каталога</summary>
        [HttpGet]
        public IActionResult GetAll() =>
            Ok(_service.GetAllSongs().Select(Map));

        /// <summary>Получить все песни конкретного диска</summary>
        [HttpGet("by-disc/{discId:guid}")]
        public IActionResult GetByDisc(Guid discId) =>
            Ok(_service.GetSongsByDisc(discId).Select(Map));

        /// <summary>Поиск песен по исполнителю во всём каталоге</summary>
        [HttpGet("search")]
        public IActionResult SearchByArtist([FromQuery] string artist) =>
            Ok(_service.SearchByArtist(artist).Select(Map));

        /// <summary>Песни диска, отсортированные по названию</summary>
        [HttpGet("by-disc/{discId:guid}/sorted-by-title")]
        public IActionResult SortedByTitle(Guid discId) =>
            Ok(_service.GetSortedByTitle(discId).Select(Map));

        /// <summary>Песни диска, отсортированные по исполнителю</summary>
        [HttpGet("by-disc/{discId:guid}/sorted-by-artist")]
        public IActionResult SortedByArtist(Guid discId) =>
            Ok(_service.GetSortedByArtist(discId).Select(Map));

        /// <summary>Добавить песню на диск</summary>
        [HttpPost]
        public IActionResult Add([FromBody] AddSongRequest request)
        {
            try
            {
                var id = _service.AddSong(
                    request.DiscId, request.Title, request.Artist,
                    request.DurationSeconds, request.TrackNumber);
                return CreatedAtAction(nameof(GetByDisc), new { discId = request.DiscId }, new { id });
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        /// <summary>Удалить песню</summary>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try { _service.DeleteSong(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        private static SongResponse Map(Song s) => new(
            s.Id, s.DiscId, s.Title, s.Artist, s.DurationSeconds,
            $"{s.DurationSeconds / 60}:{s.DurationSeconds % 60:00}",
            s.TrackNumber);
    }
}
