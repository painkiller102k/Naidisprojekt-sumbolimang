using Microsoft.Maui.Layouts;
using OopMaui.Models;

namespace OopMaui;

public partial class MainPage : ContentPage
{
    private Game? game;
    private Random rng = new();

    public MainPage()
    {
        InitializeComponent();

        ThemePicker.ItemsSource = new List<Theme>
        {
            new Theme("Light", Colors.White, Colors.Black, "Arial"),
            new Theme("Dark", Colors.Black, Colors.White, "Arial"),
            new Theme("Blue", Colors.LightBlue, Colors.DarkBlue, "Arial")
        };

        ThemePicker.SelectedIndex = 0;
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem is not Theme theme)
            return;

        theme.Apply(this);

        var player = new Player("Player", SymbolEntry.Text ?? "⭐");

        game?.Stop();

        game = new Game(player, theme, DurationSlider.Value);

        game.OnShowSymbol += ShowSymbol;
        game.OnHideSymbol += HideSymbol;
        game.OnScoreChanged += UpdateScore;
        game.OnAppearChanged += UpdateAppear;
        game.OnGameFinished += GameFinished;

        ScoreLabel.Text = "Score: 0";
        CounterLabel.Text = "Appear: 0";

        game.Start();
    }

    private void ShowSymbol(string symbol)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            double x = rng.NextDouble();
            double y = rng.NextDouble() * 0.7 + 0.2;

            SymbolLabel.Text = symbol;

            AbsoluteLayout.SetLayoutBounds(SymbolLabel, new Rect(x, y, -1, -1));
            AbsoluteLayout.SetLayoutFlags(SymbolLabel, AbsoluteLayoutFlags.PositionProportional);

            SymbolLabel.IsVisible = true;
        });
    }

    private void HideSymbol()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SymbolLabel.IsVisible = false;
        });
    }

    private void OnSymbolTapped(object sender, EventArgs e)
    {
        game?.RegisterHit();
    }

    private void UpdateScore(int score)
    {
        ScoreLabel.Text = $"Score: {score}";
    }

    private void UpdateAppear(int count)
    {
        CounterLabel.Text = $"Appear: {count}";
    }

    private void GameFinished()
    {
        DisplayAlert("Game Over", $"Score: {ScoreLabel.Text}", "OK");
    }
}