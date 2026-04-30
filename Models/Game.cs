namespace OopMaui.Models;

public class Game
{
    public Player Player { get; private set; }
    public Theme Theme { get; private set; }
    public double DurationMs { get; private set; }

    public event Action<string>? OnShowSymbol;
    public event Action? OnHideSymbol;
    public event Action<int>? OnScoreChanged;
    public event Action<int>? OnAppearChanged;
    public event Action? OnGameFinished;

    private bool isRunning;
    private int appearCount;
    private Random rng = new();

    public Game(Player player, Theme theme, double durationMs)
    {
        Player = player;
        Theme = theme;
        DurationMs = durationMs;
    }

    public async void Start()
    {
        isRunning = true;

        Player.ResetScore();
        appearCount = 0;

        var start = DateTime.Now;

        while (isRunning && (DateTime.Now - start).TotalMilliseconds < DurationMs)
        {
            appearCount++;
            OnAppearChanged?.Invoke(appearCount);

            OnShowSymbol?.Invoke(Player.Symbol);

            await Task.Delay(500);

            OnHideSymbol?.Invoke();

            await Task.Delay(rng.Next(500, 1500));
        }

        isRunning = false;
        OnGameFinished?.Invoke();
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void RegisterHit()
    {
        Player.AddPoint();
        OnScoreChanged?.Invoke(Player.Score);
    }
}