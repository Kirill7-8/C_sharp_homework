using System;

namespace MusicCatalog.Presentation.Contracts
{
    public record CreateDiscRequest(string Title, string Artist, int Year);

    public record DiscResponse(Guid Id, string Title, string Artist, int Year);
}
