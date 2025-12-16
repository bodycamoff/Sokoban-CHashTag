using System.Text.Json;

namespace Sokoban.Logic;

/// <summary>
/// Сервис для работы с файловой системой
/// Отвечает за сериализацию уровней в JSON и наоборот
/// </summary>
public class LevelService
{
    public void SaveLevel(Level level, string filePath)
    {
        var opt = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(level, opt);
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
        // TODO реализовать возможность изменять готовые уровни... потом 💀💀💀
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetLevelFiles(string foledPath)
    {
        if (!Directory.Exists(foledPath)) Directory.CreateDirectory(foledPath);
        return Directory.GetFiles(foledPath, "*.json");
    }
}
