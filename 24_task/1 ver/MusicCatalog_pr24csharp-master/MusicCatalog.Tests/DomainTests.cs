using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

namespace MusicCatalog.Tests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void Create_EmptyTitle_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                Disc.Create(""));
        }


        [TestMethod]
        public void AddSong_Duplicate_ThrowsException()
        {
            var disc = Disc.Create("Test");

            var song = Song.Create("Song", "Artist", 200, 2020);
            disc.AddSong(song);

            Assert.Throws<InvalidOperationException>(() =>
                disc.AddSong(song));
        }


        [TestMethod]
        public void SortSongsByName_SortsCorrectly()
        {
            var disc = Disc.Create("Test");

            disc.AddSong(Song.Create("B", "A", 200, 2020));
            disc.AddSong(Song.Create("A", "A", 200, 2020));

            disc.SortSongsByName();

            Assert.AreEqual("A", disc.Songs[0].Title);
        }

    }
}