using System.Text.Json;

namespace Sokoban.Logic;

public class LevelService
{
    public void SaveLevel(Level level, string filePath)
    {
        var opt = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(level, opt);
        File.WriteAllText(filePath, json);
    }

    public Level LoadLevel(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var level = JsonSerializer.Deserialize<Level>(json);
        return level;
    }

    public void UpdateLevel(string oldFileName, string newFileName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetLevelFiles(string foledPath)
    {
        if (!Directory.Exists(foledPath)) Directory.CreateDirectory(foledPath);
        return Directory.GetFiles(foledPath, "*.json");
    }
}
