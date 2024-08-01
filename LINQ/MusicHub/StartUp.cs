namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .Select(a => new 
                {
                    a.Name,
                    a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    AlbumSongs = a.Songs.Select(s => new
                    {
                        s.Name,
                        s.Price,
                        WriterName = s.Writer.Name
                    }).OrderByDescending(s => s.Name)
                        .ThenBy(s => s.WriterName).ToList(),
                    Price = a.Songs.Sum(s => s.Price)
                })
                .OrderByDescending(a => a.Price)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");

                int songNumber = 1;

                if (album.AlbumSongs.Any())
                {
                    sb.AppendLine($"-Songs:");
                    foreach (var song in album.AlbumSongs)
                    {
                        sb.AppendLine($"---#{songNumber++}");
                        sb.AppendLine($"---SongName: {song.Name}");
                        sb.AppendLine($"---Price: {song.Price:f2}");
                        sb.AppendLine($"---Writer: {song.WriterName}");
                    }
                }



                sb.AppendLine($"-AlbumPrice: {album.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var durationTimeSpan = TimeSpan.FromSeconds(duration);

            var songs = context.Songs
                .Where(s => s.Duration > durationTimeSpan)
                .Select(s => new
                {
                    s.Name,
                    Performers = 
                        s.SongPerformers
                        .Select(p => new { FullName = p.Performer.FirstName + " " + p.Performer.LastName })
                        .ToList(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    s.Duration
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.WriterName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            int songNumber = 1;

            foreach (var song in songs)
            {

                sb.AppendLine($"-Song #{songNumber++}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                


                if (song.Performers.Count >= 1)
                {
                    foreach (var performer in song.Performers.OrderBy(p => p.FullName))
                    {
                        sb.AppendLine($"---Performer: {performer.FullName}");
                    }
                }

                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration.ToString("c")}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
