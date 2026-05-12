using OopMaui.Models;

namespace OopMaui;

public partial class MainPage : ContentPage
{
    Player player;
    Game game;

    public MainPage()
    {
        InitializeComponent();

        player = new Player("Player");

        LoadTheme();

        StartGame();
    }

    void StartGame()
    {
        GameGrid.Children.Clear(); // clear cards
        game = new Game(player); 

        var values = IconPresets.Get() // võta sümbolid
            .OrderBy(x => Guid.NewGuid()) // random elements
            .ToList();

        for (int i = 0; i < values.Count; i++) 
        {
            var card = new Card(values[i]); // uus card

            var btn = new Button
            {
                Text = "❓",
                FontSize = 30
            };

            card.Button = btn;

            btn.Clicked += async (s, e) =>
            {
                await game.Pick(card);
                UpdateUI();
                CheckWin();
            };

            int row = i / 4;
            int col = i % 4; 

            GameGrid.Add(btn, col, row);
            game.Cards.Add(card);
        }
    }

    void UpdateUI()
    {
        ScoreLabel.Text = $"Score: {player.Score}";
    }

    async void CheckWin()
    {
        bool allMatched = game.Cards.All(c => c.IsMatched);

        if (!allMatched)
            return;

        bool again = await DisplayAlertAsync(
            "!",
            $"Sul on {player.Score} punkti.\nKas soovid uuesti mängida?",
            "Jah",
            "Ei");

        if (again)
        {
            player.Score = 0;
            UpdateUI();
            StartGame();
        }
    }

    // ICONS
    void Animals(object s, EventArgs e) { Icons.Save("Animals"); StartGame(); }
    void Fruits(object s, EventArgs e) { Icons.Save("Fruits"); StartGame(); }
    void Faces(object s, EventArgs e) { Icons.Save("Faces"); StartGame(); }

    // THEMES
    void Light(object s, EventArgs e)
    {
        AppThemes.Light.Apply(this);
        Preferences.Set("theme", "Light");
    }

    void Dark(object s, EventArgs e)
    {
        AppThemes.Dark.Apply(this);
        Preferences.Set("theme", "Dark");
    }

    void Color(object s, EventArgs e)
    {
        AppThemes.Color.Apply(this);
        Preferences.Set("theme", "Color");
    }

    void LoadTheme()
    {
        var t = Preferences.Get("theme", "Light");

        switch (t)
        {
            case "Dark": AppThemes.Dark.Apply(this); break;
            case "Color": AppThemes.Color.Apply(this); break;
            default: AppThemes.Light.Apply(this); break;
        }
    }
}