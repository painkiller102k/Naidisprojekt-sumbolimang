namespace OopMaui.Models;

public class Player
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public int Score { get; private set; }

    public Player(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
        Score = 0;
    }

    public void AddPoint()
    {
        Score++;
    }

    public void ResetScore()
    {
        Score = 0;
    }
}