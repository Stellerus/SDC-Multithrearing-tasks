public class ProgressBar
{
    private readonly int total;
    private readonly int width;

    public ProgressBar(int total, int width = 30)
    {
        this.total = total;
        this.width = width;
    }

    public void Report(int current)
    {
        double progress = (double)current / total;
        int filledBars = (int)(progress * width);
        string bar = new string('#', filledBars) + new string('-', width - filledBars);
        Console.CursorLeft = 0;
        Console.Write($"Progress: [{bar}] {progress * 100:0.0}%");
    }
}