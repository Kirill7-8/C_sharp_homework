using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Songs;
using MusicCatalog.Tests.Fakes;

namespace MusicCatalog.Tests.ApplicationDiskTests
{
    [TestClass]
    public class ApplicationDiskTests
    {
        [TestMethod]
        public void Handle_NewTitle_AddsDisc()
        {
            var repo = new FakeDiscRepository();
            var handler = new AddDiscHandler(repo);

            var command = new AddDiscCommand
            {
                Title = "Test Disc"
            };

            handler.Handle(command);

            var discs = repo.GetAll();
            Assert.AreEqual(1, discs.Count);
        }


        [TestMethod]
        public void Handle_DuplicateTitle_ThrowsException()
        {
            var repo = new FakeDiscRepository();
            var handler = new AddDiscHandler(repo);

            handler.Handle(new AddDiscCommand { Title = "Test" });

            Assert.Throws<InvalidOperationException>(() =>
                handler.Handle(new AddDiscCommand { Title = "Test" }));
        }
        [TestMethod]
        public void Handle_ExistingDisc_DeletesDisc()
        {
            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");
            repo.Add(disc);

            var handler = new DeleteDiscHandler(repo);

            handler.Handle(new DeleteDiscCommand { Id = disc.Id });

            Assert.AreEqual(0, repo.GetAll().Count);
        }



        [TestMethod]
        public void Handle_DiscsExist_ReturnsAllDiscs()
        {

            var repo = new FakeDiscRepository();
            repo.Add(MusicCatalog.Domain.Discs.Disc.Create("A"));
            repo.Add(MusicCatalog.Domain.Discs.Disc.Create("B"));

            var handler = new GetAllDiscsHandler(repo);


            var result = handler.Handle(new GetAllDiscsQuery());
            Assert.AreEqual(2, result.Count);
        }


        [TestMethod]
        public void Handle_ValidId_ReturnsDisc()
        {    

            var repo = new FakeDiscRepository();
            var disc = MusicCatalog.Domain.Discs.Disc.Create("Test");
            repo.Add(disc);
            var handler = new GetDiscByIdHandler(repo);

            var result = handler.Handle(new GetDiscByIdQuery { Id = disc.Id });


            Assert.AreEqual(disc.Id, result.Id);
        }
    }
}
