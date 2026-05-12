using Microsoft.Maui.Storage;

namespace OopMaui.Models;

public static class Icons
{
    public static void Save(string name)
    {
        Preferences.Set("icons", name);
    }

    public static string Load() 
    {
        return Preferences.Get("icons", "Animals");
    }
}