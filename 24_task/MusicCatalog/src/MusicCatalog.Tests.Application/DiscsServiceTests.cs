using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Tests.Application
{
    [TestFixture]
    public class DiscsServiceTests
    {
        private Mock<IDiscsRepository> _discsRepoMock;
        private Mock<ISongsRepository> _songsRepoMock;
        private DiscsService           _service;

        [SetUp]
        public void SetUp()
        {
            _discsRepoMock = new Mock<IDiscsRepository>();
            _songsRepoMock = new Mock<ISongsRepository>();
            _service       = new DiscsService(_discsRepoMock.Object, _songsRepoMock.Object);
        }

        // ─── CreateDisc ──────────────────────────────────────────────────

        [Test]
        public void CreateDisc_ValidData_CallsAddOnce()
        {
            _service.CreateDisc("Thriller", "Michael Jackson", 1982);
            _discsRepoMock.Verify(r => r.Add(It.IsAny<Disc>()), Times.Once);
        }

        [Test]
        public void CreateDisc_ValidData_ReturnsNonEmptyGuid()
        {
            var id = _service.CreateDisc("Thriller", "Michael Jackson", 1982);
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void CreateDisc_AddedDiscHasCorrectTitleAndArtist()
        {
            Disc added = null;
            _discsRepoMock.Setup(r => r.Add(It.IsAny<Disc>()))
                          .Callback<Disc>(d => added = d);

            _service.CreateDisc("Thriller", "Michael Jackson", 1982);

            Assert.That(added.Title,  Is.EqualTo("Thriller"));
            Assert.That(added.Artist, Is.EqualTo("Michael Jackson"));
        }

        // ─── GetDisc ─────────────────────────────────────────────────────

        [Test]
        public void GetDisc_ExistingId_ReturnsDisc()
        {
            var expected = Disc.Create("Thriller", "Michael Jackson", 1982);
            _discsRepoMock.Setup(r => r.GetById(expected.Id)).Returns(expected);

            var result = _service.GetDisc(expected.Id);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetDisc_NotFound_ThrowsException()
        {
            var unknownId = Guid.NewGuid();
            _discsRepoMock.Setup(r => r.GetById(unknownId)).Returns((Disc)null);

            var ex = Assert.Throws<Exception>(() => _service.GetDisc(unknownId));
            Assert.That(ex.Message, Does.Contain(unknownId.ToString()));
        }

        // ─── DeleteDisc ──────────────────────────────────────────────────

        [Test]
        public void DeleteDisc_ExistingId_CallsDeleteOnce()
        {
            var disc = Disc.Create("Thriller", "Michael Jackson", 1982);
            _discsRepoMock.Setup(r => r.GetById(disc.Id)).Returns(disc);
            _songsRepoMock.Setup(r => r.GetByDiscId(disc.Id)).Returns(new List<Song>());

            _service.DeleteDisc(disc.Id);

            _discsRepoMock.Verify(r => r.Delete(disc.Id), Times.Once);
        }

        [Test]
        public void DeleteDisc_NotFound_ThrowsException()
        {
            var unknownId = Guid.NewGuid();
            _discsRepoMock.Setup(r => r.GetById(unknownId)).Returns((Disc)null);

            Assert.Throws<Exception>(() => _service.DeleteDisc(unknownId));
        }

        [Test]
        public void DeleteDisc_WithSongs_AlsoDeletesSongs()
        {
            var disc = Disc.Create("Thriller", "Michael Jackson", 1982);
            var song = Song.Create(disc.Id, "Billie Jean", "Michael Jackson", 294, 2);

            _discsRepoMock.Setup(r => r.GetById(disc.Id)).Returns(disc);
            _songsRepoMock.Setup(r => r.GetByDiscId(disc.Id)).Returns(new List<Song> { song });

            _service.DeleteDisc(disc.Id);

            _songsRepoMock.Verify(r => r.Delete(song.Id), Times.Once);
        }

        // ─── GetAllDiscs ─────────────────────────────────────────────────

        [Test]
        public void GetAllDiscs_CallsRepository()
        {
            _discsRepoMock.Setup(r => r.GetAll()).Returns(new List<Disc>());
            _service.GetAllDiscs();
            _discsRepoMock.Verify(r => r.GetAll(), Times.Once);
        }
    }
}
