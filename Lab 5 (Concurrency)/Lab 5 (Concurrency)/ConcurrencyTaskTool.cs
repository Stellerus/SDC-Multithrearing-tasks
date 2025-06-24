using BikeLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Handles concurrent tasks related to bike generation, serialization, deserialization,
/// progress tracking, and periodic sorting display.
/// </summary>
internal class ConcurrencyTaskTool
{
    // Dependencies
    private readonly BikeGenerator bikeGenerator = new();
    private readonly BikeSerializer bikeSerializer = new();
    private ProgressBar progressBar;

    private List<Bike> allBikes;
    private const int numberOfBikes = 50;

    // File names for serialized bike groups
    private readonly string[] fileNames = new[]
    {
        "bikesSerializedPart1.xml",
        "bikesSerializedPart2.xml",
        "bikesSerializedPart3.xml",
        "bikesSerializedPart4.xml",
        "bikesSerializedPart5.xml"
    };

    // Output file with all bikes merged
    private const string allBikesSerialized = "allBikesSerialized.xml";

    // Thread-safe dictionary to hold file data in memory
    private readonly ConcurrentDictionary<string, ConcurrentQueue<Bike>> fileDictionary = new();

    // For progressBar Functionality
    private int recordsRead = 0;
    private int totalRecords = 0;

    private readonly CancellationTokenSource sortCts = new();

    public ConcurrencyTaskTool()
    {
        allBikes = bikeGenerator.Generate(numberOfBikes);
        totalRecords = allBikes.Count;
        progressBar = new ProgressBar(totalRecords);
    }

    /// <summary>
    /// Splits generated bikes into 5 groups and serializes each group into a separate file asynchronously.
    /// </summary>
    public async Task GenerateFilesAsync()
    {
        var groups = allBikes
            .Select((bike, index) => new { bike, index })
            .GroupBy(x => x.index / 10)
            .Select(g => g.Select(x => x.bike).ToList())
            .ToList();

        var tasks = groups.Select((group, idx) =>
            Task.Run(() => bikeSerializer.Serialize(group, fileNames[idx])))
            .ToArray();

        await Task.WhenAll(tasks);
        Console.WriteLine($"{numberOfBikes} instances are spread across 5 files");
    }

    /// <summary>
    /// Reads all serialized files concurrently, tracks progress and stores contents into a dictionary.
    /// Also merges all bikes into a single file and prints their contents.
    /// </summary>
    public async Task ReadFilesConcurrentlyAsync()
    {
        fileDictionary.Clear();
        recordsRead = 0;

        var tasks = fileNames.Select(file => Task.Run(() => 
                ReadFileAndPopulateDictionaryAsync(file)))
                .ToArray();

        await Task.WhenAll(tasks);

        MergeDictionaryToFile();
        Console.WriteLine("\nCombined file created.");

        // kvp - Key Value Pair
        foreach (var kvp in fileDictionary)
        {
            Console.WriteLine($"\nContents of file [{kvp.Key}]:");
            foreach (var bike in kvp.Value)
            {
                Console.WriteLine($"Bike ID: {bike.ID}, Type: {bike.BikeType}");
            }
        }
    }

    /// <summary>
    /// Reads and deserializes bikes from a single file asynchronously,
    /// queues the bikes and reports progress.
    /// </summary>
    private async Task ReadFileAndPopulateDictionaryAsync(string fileName)
    {
        var bikesFromFile = bikeSerializer.Deserialize(fileName);
        var queue = new ConcurrentQueue<Bike>();

        foreach (var bike in bikesFromFile)
        {
            queue.Enqueue(bike);
            Interlocked.Increment(ref recordsRead);
            progressBar.Report(recordsRead);

            await Task.Delay(500); // half a second
        }

        fileDictionary.TryAdd(fileName, queue);
    }

    /// <summary>
    /// Merges all bike entries from the dictionary into a single output file.
    /// </summary>
    private void MergeDictionaryToFile()
    {
        var mergedBikes = fileDictionary.Values
                          .SelectMany(q => q)
                          .ToList();

        bikeSerializer.Serialize(mergedBikes, allBikesSerialized);
    }

    /// <summary>
    /// Starts a background task that periodically sorts and displays the bike IDs in each file queue.
    /// </summary>
    public void StartSortingHandler()
    {
        Task.Run(async () =>
        {
            while (!sortCts.Token.IsCancellationRequested)
            {
                foreach (var key in fileDictionary.Keys)
                {
                    if (fileDictionary.TryGetValue(key, out var queue))
                    {
                        var sorted = queue
                                    .ToList()
                                    .OrderBy(b => b.ID.ToString())
                                    .ToList();

                        Console.WriteLine($"\n[Sorting] File {key}:");
                        foreach (var bike in sorted)
                        {
                            Console.Write($"[{bike.ID}] ");
                        }
                        Console.WriteLine();
                    }
                }
                await Task.Delay(1000);
            }
        }, sortCts.Token);
    }

    /// <summary>
    /// Stops the background sorting handler task.
    /// </summary>
    public void StopSortingHandler()
    {
        if (!sortCts.IsCancellationRequested)
        {
            sortCts.Cancel();
        }
    }
}
