using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.Discs;
using MusicCatalog.Domain.Discs;
using MusicCatalog.Presentation.Contracts;

namespace MusicCatalog.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscsController : ControllerBase
    {
        private readonly DiscsService _service;

        public DiscsController(DiscsService service) => _service = service;

        /// <summary>Получить весь каталог дисков</summary>
        [HttpGet]
        public IActionResult GetAll() =>
            Ok(_service.GetAllDiscs().Select(Map));

        /// <summary>Получить диск по Id</summary>
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            try { return Ok(Map(_service.GetDisc(id))); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        /// <summary>Добавить диск в каталог</summary>
        [HttpPost]
        public IActionResult Create([FromBody] CreateDiscRequest request)
        {
            try
            {
                var id = _service.CreateDisc(request.Title, request.Artist, request.Year);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        /// <summary>Удалить диск и все его песни</summary>
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try { _service.DeleteDisc(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        private static DiscResponse Map(Disc d) => new(d.Id, d.Title, d.Artist, d.Year);
    }
}
