namespace Sokoban.Logic;

/// <summary>
/// Класс контейнер для хранения состояния уровня (DTO)
/// </summary>
public class Level
{
    public string Name { get; set; } = "New Level";

    public int Heigth { get; set; }
    public int Width { get; set; }

    public int[] MapLayout { get; set; }

    public int PlayerStartX { get; set; }
    public int PlayerStartY { get; set; }
    public List<Box> InitialBoxes { get; set; } = new();

    public bool IsCompleted { get; set; }
    public int BestSteps { get; set; } = 0;
}


