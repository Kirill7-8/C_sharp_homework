

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Songs;
using MusicCatalog.Tests.Fakes;

namespace MusicCatalog.Tests.ApplicationSongTests
{
    [TestClass]
    public class ApplicationSongTests
    {
        [TestMethod]
        public void Handle_ValidData_AddsSongToDisc()
        {
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");
            repo.Add(disc);

            var handler = new AddSongToDiscHandler(repo);

            var command = new AddSongToDiscCommand
            {
                DiscId = disc.Id,
                Title = "Song",
                Artist = "Artist",
                Durration = 200,
                Year = 2020
            };

            handler.Handle(command);

            var updatedDisc = repo.GetById(disc.Id);
            Assert.AreEqual(1, updatedDisc.Songs.Count);
        }


        [TestMethod]
        public void Handle_DiscNotFound_ThrowsException()
        {
            var repo = new FakeDiscRepository();
            var handler = new AddSongToDiscHandler(repo);

            var command = new AddSongToDiscCommand
            {
                DiscId = Guid.NewGuid(),
                Title = "Song",
                Artist = "Artist",
                Durration = 200,
                Year = 2020
            };

            Assert.Throws<InvalidOperationException>(() =>
                handler.Handle(command));
        }

        [TestMethod]
        public void Handle_ValidData_RemovesSong()
        {
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");

            var song = MusicCatalog.Domain.Songs.Song.Create("Song", "Artist", 200, 2020);
            disc.AddSong(song);

            repo.Add(disc);

            var handler = new DeleteSongHandler(repo);

            handler.Handle(new DeleteSongCommand
            {
                DiscId = disc.Id,
                SongId = song.Id
            });

            var updated = repo.GetById(disc.Id);
            Assert.AreEqual(0, updated.Songs.Count);
        }

        [TestMethod]
        public void Handle_SortSongsByName_SortsCorrectly()
        {    
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");

            disc.AddSong(MusicCatalog.Domain.Songs.Song.Create("B", "Artist", 200, 2020));
            disc.AddSong(MusicCatalog.Domain.Songs.Song.Create("A", "Artist", 200, 2020));
            repo.Add(disc);

            var handler = new SortSongsByNameHandler(repo);

            handler.Handle(new SortSongsByNameCommand { DiscId = disc.Id });

            var updated = repo.GetById(disc.Id);
            Assert.AreEqual("A", updated.Songs[0].Title);
        }



        [TestMethod]
        public void Handle_SortSongsByArtist_SortsCorrectly()
        {
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");

            disc.AddSong(MusicCatalog.Domain.Songs.Song.Create("Song1", "B", 200, 2020));
            disc.AddSong(MusicCatalog.Domain.Songs.Song.Create("Song2", "A", 200, 2020));

            repo.Add(disc); var handler = new SortSongsByArtistHandler(repo);

            handler.Handle(new SortSongsByArtistCommand { DiscId = disc.Id });
            var updated = repo.GetById(disc.Id);

            Assert.AreEqual("A", updated.Songs[0].Artist);
        }

        [TestMethod]
        public void Handle_ExistingArtist_ReturnsSongs()
        {
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");

            disc.AddSong(MusicCatalog.Domain.Songs.Song.Create("Song", "Artist", 200, 2020));
            repo.Add(disc); var handler = new SearchSongsByArtistHandler(repo);

            var result = handler.Handle(new SearchSongsByArtistQuery { Artist = "Artist" });

            Assert.AreEqual(1, result.Count);
        }


    }
}