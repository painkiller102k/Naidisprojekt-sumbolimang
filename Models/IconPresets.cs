namespace OopMaui.Models;

public static class IconPresets
{
    public static List<string> Get()
    {
        return Icons.Load() switch
        {
            "Fruits" => new() { "🍎", "🍎", "🍌", "🍌", "🍇", "🍇", "🍉", "🍉" },
            "Faces" => new() { "😀", "😀", "😎", "😎", "😍", "😍", "🤖", "🤖" },
            _ => new() { "🐶", "🐶", "🐱", "🐱", "🐰", "🐰", "🐵", "🐵" }
        };
    }
}