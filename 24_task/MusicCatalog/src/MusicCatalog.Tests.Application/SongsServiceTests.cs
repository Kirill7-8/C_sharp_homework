using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using MusicCatalog.Application.Songs;
using MusicCatalog.Application.Songs.Abstractions;
using MusicCatalog.Application.Discs.Abstractions;
using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Tests.Application
{
    [TestFixture]
    public class SongsServiceTests
    {
        private Mock<ISongsRepository> _songsRepoMock;
        private Mock<IDiscsRepository> _discsRepoMock;
        private SongsService           _service;

        private Disc _disc;

        [SetUp]
        public void SetUp()
        {
            _songsRepoMock = new Mock<ISongsRepository>();
            _discsRepoMock = new Mock<IDiscsRepository>();
            _service       = new SongsService(_songsRepoMock.Object, _discsRepoMock.Object);

            _disc = Disc.Create("Thriller", "Michael Jackson", 1982);
            _discsRepoMock.Setup(r => r.GetById(_disc.Id)).Returns(_disc);
        }

        // ─── AddSong ─────────────────────────────────────────────────────

        [Test]
        public void AddSong_ValidData_CallsAddOnce()
        {
            _service.AddSong(_disc.Id, "Billie Jean", "Michael Jackson", 294, 2);
            _songsRepoMock.Verify(r => r.Add(It.IsAny<Song>()), Times.Once);
        }

        [Test]
        public void AddSong_ValidData_ReturnsNonEmptyGuid()
        {
            var id = _service.AddSong(_disc.Id, "Billie Jean", "Michael Jackson", 294, 2);
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void AddSong_DiscNotFound_ThrowsException()
        {
            var unknownDiscId = Guid.NewGuid();
            _discsRepoMock.Setup(r => r.GetById(unknownDiscId)).Returns((Disc)null);

            var ex = Assert.Throws<Exception>(() =>
                _service.AddSong(unknownDiscId, "Song", "Artist", 200, 1));
            Assert.That(ex.Message, Does.Contain(unknownDiscId.ToString()));
        }

        // ─── DeleteSong ──────────────────────────────────────────────────

        [Test]
        public void DeleteSong_ExistingId_CallsDeleteOnce()
        {
            var song = Song.Create(_disc.Id, "Billie Jean", "Michael Jackson", 294, 2);
            _songsRepoMock.Setup(r => r.GetById(song.Id)).Returns(song);

            _service.DeleteSong(song.Id);

            _songsRepoMock.Verify(r => r.Delete(song.Id), Times.Once);
        }

        [Test]
        public void DeleteSong_NotFound_ThrowsException()
        {
            var unknownId = Guid.NewGuid();
            _songsRepoMock.Setup(r => r.GetById(unknownId)).Returns((Song)null);

            Assert.Throws<Exception>(() => _service.DeleteSong(unknownId));
        }

        // ─── SearchByArtist ──────────────────────────────────────────────

        [Test]
        public void SearchByArtist_CallsRepositoryWithCorrectArtist()
        {
            _songsRepoMock.Setup(r => r.GetByArtist("Pink Floyd")).Returns(new List<Song>());

            _service.SearchByArtist("Pink Floyd");

            _songsRepoMock.Verify(r => r.GetByArtist("Pink Floyd"), Times.Once);
        }

        // ─── GetSortedByTitle ────────────────────────────────────────────

        [Test]
        public void GetSortedByTitle_ReturnsSongsInAlphabeticalOrder()
        {
            var s1 = Song.Create(_disc.Id, "Money",          "Pink Floyd", 382, 6);
            var s2 = Song.Create(_disc.Id, "Brain Damage",   "Pink Floyd", 228, 9);
            var s3 = Song.Create(_disc.Id, "Time",           "Pink Floyd", 421, 4);

            _songsRepoMock.Setup(r => r.GetByDiscId(_disc.Id))
                          .Returns(new List<Song> { s1, s2, s3 });

            var result = _service.GetSortedByTitle(_disc.Id);

            Assert.That(result[0].Title, Is.EqualTo("Brain Damage"));
            Assert.That(result[1].Title, Is.EqualTo("Money"));
            Assert.That(result[2].Title, Is.EqualTo("Time"));
        }

        // ─── GetSortedByArtist ───────────────────────────────────────────

        [Test]
        public void GetSortedByArtist_ReturnsSongsInAlphabeticalOrder()
        {
            var s1 = Song.Create(_disc.Id, "Song 1", "Цой",     200, 1);
            var s2 = Song.Create(_disc.Id, "Song 2", "Агутин",  210, 2);
            var s3 = Song.Create(_disc.Id, "Song 3", "Меладзе", 220, 3);

            _songsRepoMock.Setup(r => r.GetByDiscId(_disc.Id))
                          .Returns(new List<Song> { s1, s2, s3 });

            var result = _service.GetSortedByArtist(_disc.Id);

            Assert.That(result[0].Artist, Is.EqualTo("Агутин"));
            Assert.That(result[1].Artist, Is.EqualTo("Меладзе"));
            Assert.That(result[2].Artist, Is.EqualTo("Цой"));
        }

        // ─── GetSongsByDisc ──────────────────────────────────────────────

        [Test]
        public void GetSongsByDisc_CallsRepositoryWithCorrectDiscId()
        {
            _songsRepoMock.Setup(r => r.GetByDiscId(_disc.Id)).Returns(new List<Song>());

            _service.GetSongsByDisc(_disc.Id);

            _songsRepoMock.Verify(r => r.GetByDiscId(_disc.Id), Times.Once);
        }
    }
}
