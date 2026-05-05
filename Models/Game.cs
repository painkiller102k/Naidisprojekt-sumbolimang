namespace OopMaui.Models;

public class Game
{
    public Player Player { get; set; }
    public List<Card> Cards { get; set; } = new();

    Card first;
    Card second;
    bool busy;

    public Game(Player player)
    {
        Player = player;
    }

    public async Task Pick(Card card)
    {
        if (busy || card.IsMatched || card.IsFlipped)
            return;

        busy = true;

        await Open(card);

        if (first == null)
        {
            first = card;
            busy = false;
            return;
        }

        second = card;

        await Task.Delay(500);

        if (first.Value == second.Value)
        {
            Player.Score++;

            first.IsMatched = true;
            second.IsMatched = true;

            await Pulse(first.Button);
            await Pulse(second.Button);
        }
        else
        {
            Player.Score--;

            await Close(first);
            await Close(second);
        }

        first = null;
        second = null;
        busy = false;
    }


    async Task Open(Card c)
    {
        await c.Button.ScaleToAsync(0.2, 80);
        c.IsFlipped = true;
        c.Button.Text = c.Value;
        await c.Button.ScaleToAsync(1, 80);
    }

    async Task Close(Card c)
    {
        await c.Button.ScaleToAsync(0.2, 80);
        c.IsFlipped = false;
        c.Button.Text = "❓";
        await c.Button.ScaleToAsync(1, 80);
    }

    async Task Pulse(Button btn)
    {
        await btn.ScaleToAsync(1.2, 100);
        await btn.ScaleToAsync(1, 100);
    }
}