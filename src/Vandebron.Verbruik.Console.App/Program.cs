using CommandLine;
using CsvHelper;
using System.Globalization;
using Vandebron.Verbruik.Console.App.Api.Usage;

namespace Vandebron.Verbruik.Console.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await 
                Parser
                .Default
                .ParseArguments<Options>(args)
                .WithParsedAsync(async o =>
                {
                    using (var downloader = new VerbruikDownloader())
                    {
                        var usage = new List<Entry>();
                        await foreach (var entry in downloader.Download(o.ConsumerId, o.ConnectionId, o.Token))
                        {
                            usage.Add(entry);
                        }

                        var path = GetPath();
                        using (var writer = new StreamWriter(path))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            await csv.WriteRecordsAsync(usage.OrderBy(u => u.Time));
                        }
                    }
                });
        }


        static string GetPath()
        {
            return "output.csv";
        }
    }
}