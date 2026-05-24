using System;
using NUnit.Framework;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Tests.Domain
{
    [TestFixture]
    public class SongTests
    {
        private static readonly Guid DiscId = Guid.NewGuid();

        // ─── Позитивные тесты ────────────────────────────────────────────

        [Test]
        public void Create_ValidData_ReturnsSong()
        {
            var song = Song.Create(DiscId, "Money", "Pink Floyd", 382, 6);
            Assert.IsNotNull(song);
        }

        [TestCase("Money",           "Pink Floyd",      382, 6)]
        [TestCase("Come Together",   "The Beatles",     259, 1)]
        [TestCase("Billie Jean",     "Michael Jackson", 294, 2)]
        public void Create_ValidData_SetsPropertiesCorrectly(
            string title, string artist, int duration, int track)
        {
            var song = Song.Create(DiscId, title, artist, duration, track);

            Assert.That(song.Title,           Is.EqualTo(title));
            Assert.That(song.Artist,          Is.EqualTo(artist));
            Assert.That(song.DurationSeconds, Is.EqualTo(duration));
            Assert.That(song.TrackNumber,     Is.EqualTo(track));
            Assert.That(song.DiscId,          Is.EqualTo(DiscId));
            Assert.That(song.Id,              Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Create_TwoSongs_HaveDifferentIds()
        {
            var s1 = Song.Create(DiscId, "Song A", "Artist", 200, 1);
            var s2 = Song.Create(DiscId, "Song B", "Artist", 200, 2);
            Assert.That(s1.Id, Is.Not.EqualTo(s2.Id));
        }

        // ─── Негативные тесты ────────────────────────────────────────────

        [Test]
        public void Create_EmptyDiscId_ThrowsException()
        {
            var ex = Assert.Throws<Exception>(() =>
                Song.Create(Guid.Empty, "Title", "Artist", 200, 1));
            Assert.That(ex.Message, Is.EqualTo("Не указан диск для песни"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Create_EmptyTitle_ThrowsException(string title)
        {
            var ex = Assert.Throws<Exception>(() =>
                Song.Create(DiscId, title, "Artist", 200, 1));
            Assert.That(ex.Message, Is.EqualTo("Название песни не может быть пустым"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Create_EmptyArtist_ThrowsException(string artist)
        {
            var ex = Assert.Throws<Exception>(() =>
                Song.Create(DiscId, "Title", artist, 200, 1));
            Assert.That(ex.Message, Is.EqualTo("Исполнитель песни не может быть пустым"));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Create_ZeroOrNegativeDuration_ThrowsException(int duration)
        {
            var ex = Assert.Throws<Exception>(() =>
                Song.Create(DiscId, "Title", "Artist", duration, 1));
            Assert.That(ex.Message, Is.EqualTo("Длительность песни должна быть больше нуля"));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Create_ZeroOrNegativeTrackNumber_ThrowsException(int track)
        {
            var ex = Assert.Throws<Exception>(() =>
                Song.Create(DiscId, "Title", "Artist", 200, track));
            Assert.That(ex.Message, Is.EqualTo("Номер трека должен быть больше нуля"));
        }
    }
}
