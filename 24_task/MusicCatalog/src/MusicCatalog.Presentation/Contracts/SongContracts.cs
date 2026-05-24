using System;

namespace MusicCatalog.Presentation.Contracts
{
    public record AddSongRequest(
        Guid   DiscId,
        string Title,
        string Artist,
        int    DurationSeconds,
        int    TrackNumber);

    public record SongResponse(
        Guid   Id,
        Guid   DiscId,
        string Title,
        string Artist,
        int    DurationSeconds,
        string Duration,
        int    TrackNumber);
}
