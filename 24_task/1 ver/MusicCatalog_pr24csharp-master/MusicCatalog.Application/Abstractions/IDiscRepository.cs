
using MusicCatalog.Domain.Discs;
using MusicCatalog.Domain.Songs;

//Реализовать каталог музыкальных компакт-дисков, который позволяет: добавлять и удалять
//диски; добавлять и удалять песни на диске; просматривать содержимое целого каталога и
//каждого диска в отдельности; осуществлять поиск всех песен заданного исполнителя по всему
//каталогу; сортировать песни на каждом диске в алфавитном порядке названий песен или
//фамилий исполнителей.

namespace MusicCatalog.Application.Abstractions
{
    public interface IDiscRepository
    {
        void Add(Disc disc); //добавить диск
        void Delete(Guid id); //удалить диск
        Disc GetById(Guid id); //найти по айди
        List<Disc> GetAll(); //все диски
        void Update(Disc disc); //обновить диск
        List<Song> GetAllSongsByArtist(string artist); //найти все песни исполнителя
        Disc GetByTitle(string title); //поиск диска по названию (для проверки на дубликаты) 
    }
}