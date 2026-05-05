namespace OopMaui.Models;

public static class AppThemes
{
    public static Theme Light = new(
        "Light",
        Colors.White,
        Colors.LightGray,
        "Arial");

    public static Theme Dark = new(
        "Dark",
        Colors.Black,
        Colors.MediumPurple,
        "Arial");

    public static Theme Color = new(
        "Color",
        Colors.LightGreen,
        Colors.YellowGreen,
        "Comic Sans MS");
}