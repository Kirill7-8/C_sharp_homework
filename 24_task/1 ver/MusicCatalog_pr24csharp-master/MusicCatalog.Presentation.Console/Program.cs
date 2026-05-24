using System;
using System.Text.Json;
using MusicCatalog.Application.Discs;
using MusicCatalog.Application.Songs;
using MusicCatalog.Infrastructure.Discs;

namespace MusicCatalog.Presentation.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new DiscRepository();
            var addDiscHandler = new AddDiscHandler(repository);
            var getAllDiscsHandler = new GetAllDiscsHandler(repository);
            var addSongHandler = new AddSongToDiscHandler(repository);
            var searchSongsHandler = new SearchSongsByArtistHandler(repository);
            var sortByNameHandler = new SortSongsByNameHandler(repository);
            var sortByArtistHandler = new SortSongsByArtistHandler(repository);

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

        //добавление диска
            addDiscHandler.Handle(new AddDiscCommand { Title = "ROCK HITS" });
            System.Console.WriteLine("Диск 'ROCK HITS' добавлен\n");

         //добавление песен на диск 
            var discs = getAllDiscsHandler.Handle(new GetAllDiscsQuery());
            var discId = discs[0].Id;

            addSongHandler.Handle(new AddSongToDiscCommand
            {
                DiscId = discId,
                Title = "Bohemian Rhapsody",
                Artist = "Queen"
            });
            System.Console.WriteLine("Добавлена песня 'Bohemian Rhapsody' - Queen");

            addSongHandler.Handle(new AddSongToDiscCommand
            {
                DiscId = discId,
                Title = "Stairway to Heaven",
                Artist = "Led Zeppelin"
            });
            System.Console.WriteLine("добавлена песня 'Stairway to Heaven' - Led Zeppelin");

            addSongHandler.Handle(new AddSongToDiscCommand
            {
                DiscId = discId,
                Title = "Back in Black",
                Artist = "AC/DC"
            });
            System.Console.WriteLine("Добавлена песня 'Back in Black' - AC/DC\n");

            //просмотр содержимого диска
            System.Console.WriteLine("Содержимое диска:");
            var updatedDisc = repository.GetById(discId);
            System.Console.WriteLine($"Диск: {updatedDisc.Title}");
            foreach (var song in updatedDisc.Songs)
            {
                System.Console.WriteLine($"  - {song.Title} ({song.Artist})");
            }
            System.Console.WriteLine();

            //сортировка по названию
            System.Console.WriteLine("Сортировка песен по названию:");
            sortByNameHandler.Handle(new SortSongsByNameCommand { DiscId = discId });
            var sortedByName = repository.GetById(discId);
            foreach (var song in sortedByName.Songs)
            {
                System.Console.WriteLine($"  - {song.Title} ({song.Artist})");
            }
            System.Console.WriteLine();

            //сортировка по исполнителю
            System.Console.WriteLine("Сортировка песен по исполнителю:");
            sortByArtistHandler.Handle(new SortSongsByArtistCommand { DiscId = discId });
            var sortedByArtist = repository.GetById(discId);
            foreach (var song in sortedByArtist.Songs)
            {
                System.Console.WriteLine($"  - {song.Artist} - {song.Title}");
            }
            System.Console.WriteLine();

            //поиск песен по исполнителю
            System.Console.WriteLine("Поиск песен исполнителя 'Queen':");
            var searchResult = searchSongsHandler.Handle(new SearchSongsByArtistQuery { Artist = "Queen" });
            foreach (var song in searchResult)
            {
                System.Console.WriteLine($"  - {song.Title}");
            }
            System.Console.WriteLine();

            //все диски в каталоге
            System.Console.WriteLine("Все диски в каталоге:");
            foreach (var disc in repository.GetAll())
            {
                System.Console.WriteLine($"\nДиск: {disc.Title}");
                System.Console.WriteLine($"Количество песен: {disc.Songs.Count}");
                foreach (var song in disc.Songs)
                {
                    System.Console.WriteLine($"  - {song.Title} ({song.Artist})");
                }
            }
        }
    }
}