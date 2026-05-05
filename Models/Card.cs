namespace OopMaui.Models;

public class Card
{
    public string Value { get; set; }
    public bool IsFlipped { get; set; }
    public bool IsMatched { get; set; }
    public Button Button { get; set; }

    public Card(string value)
    {
        Value = value;
    }
}