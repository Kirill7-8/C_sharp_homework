using System;
using NUnit.Framework;
using MusicCatalog.Domain.Discs;

namespace MusicCatalog.Tests.Domain
{
    [TestFixture]
    public class DiscTests
    {
        // ─── Позитивные тесты ────────────────────────────────────────────

        [Test]
        public void Create_ValidData_ReturnsDisc()
        {
            var disc = Disc.Create("The Dark Side of the Moon", "Pink Floyd", 1973);
            Assert.IsNotNull(disc);
        }

        [TestCase("The Dark Side of the Moon", "Pink Floyd",    1973)]
        [TestCase("Abbey Road",                "The Beatles",   1969)]
        [TestCase("Thriller",                  "Michael Jackson", 1982)]
        public void Create_ValidData_SetsPropertiesCorrectly(string title, string artist, int year)
        {
            var disc = Disc.Create(title, artist, year);

            Assert.That(disc.Title,  Is.EqualTo(title));
            Assert.That(disc.Artist, Is.EqualTo(artist));
            Assert.That(disc.Year,   Is.EqualTo(year));
            Assert.That(disc.Id,     Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Create_TwoDiscs_HaveDifferentIds()
        {
            var d1 = Disc.Create("Album A", "Artist A", 2000);
            var d2 = Disc.Create("Album B", "Artist B", 2001);
            Assert.That(d1.Id, Is.Not.EqualTo(d2.Id));
        }

        // ─── Негативные тесты ────────────────────────────────────────────

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Create_EmptyTitle_ThrowsException(string title)
        {
            var ex = Assert.Throws<Exception>(() => Disc.Create(title, "Artist", 2000));
            Assert.That(ex.Message, Is.EqualTo("Название диска не может быть пустым"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Create_EmptyArtist_ThrowsException(string artist)
        {
            var ex = Assert.Throws<Exception>(() => Disc.Create("Title", artist, 2000));
            Assert.That(ex.Message, Is.EqualTo("Исполнитель диска не может быть пустым"));
        }

        [TestCase(1899)]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_InvalidYear_ThrowsException(int year)
        {
            var ex = Assert.Throws<Exception>(() => Disc.Create("Title", "Artist", year));
            Assert.That(ex.Message, Does.Contain("Некорректный год"));
        }
    }
}
