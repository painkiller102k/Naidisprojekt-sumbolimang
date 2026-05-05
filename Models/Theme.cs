namespace OopMaui.Models;
using Microsoft.Maui.Controls;

public class Theme
{
    public string Name { get; set; }
    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }
    public string FontFamily { get; set; }

    public Theme(string name, Color bg, Color text, string font)
    {
        Name = name;
        BackgroundColor = bg;
        TextColor = text;
        FontFamily = font;
    }

    public override string ToString() => Name;

    public void Apply(ContentPage page)
    {
        page.BackgroundColor = BackgroundColor;

        foreach (var view in page.GetVisualTreeDescendants())
        {
            if (view is Label label)
            {
                label.TextColor = TextColor;
                label.FontFamily = FontFamily;
            }

            if (view is Button button)
            {
                button.TextColor = TextColor;
            }
        }
    }
}