using static Sokoban.Logic.Enums;

namespace Sokoban.Logic;

public class Game
{
    public CellType[,] Map {  get; set; }
    public int Width => Map.GetLength(1);
    public int Height=> Map.GetLength(0);
    public Player Player { get; private set; }
    public List<Box> Boxes { get; private set; }
    public int Steps { get; private set; }
    public bool IsCompleted { get; private set; }

    public Game(Level lvl)
    {
        Map = new CellType[lvl.Heigth, lvl.Width];
        for (int y = 0; y < lvl.Heigth; y++)
        for (int x = 0; x < lvl.Width; x++)
        {
            var typeIndex = lvl.MapLayout[y * lvl.Width + x];
            Map[y, x] = (CellType)typeIndex;
        }

        Player = new Player(lvl.PlayerStartX, lvl.PlayerStartY);
        Boxes = new List<Box>();

        foreach (var box in lvl.InitialBoxes)
            Boxes.Add(new Box(box.X, box.Y));

        Steps = 0;
    }

    public CellType ParseSymbol(int c) => (CellType)c;

    public void Move(Direction direction)
    {
        var dx = 0;
        var dy = 0;
        switch(direction)
        {
            case Direction.Up: dy = - 1; break;
            case Direction.Down: dy = 1; break;
            case Direction.Left: dx = -1; break;
            case Direction.Right: dx = 1; break;
        }
        var newX = Player.X + dx;
        var newY = Player.Y + dy;

        if (Map[newY, newX] == CellType.Wall) return;

        var box = Boxes.FirstOrDefault(b => b.X == newX && b.Y == newY);

        if (box != null)
        {
            int boxNewX = box.X + dx;
            int boxNewY = box.Y + dy;
            if (!IsValidPosition(boxNewX, boxNewY)) return;
            if (Map[boxNewY, boxNewX] == CellType.Wall) return;
            if (Boxes.Any(b => b.X == boxNewX && b.Y == boxNewY)) return;
            box.X = boxNewX; box.Y = boxNewY;
        }

        Player.X = newX; Player.Y = newY;
        Steps++;
        CheckWin();
    }

    private bool IsValidPosition(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    public void CheckWin()
    {
        for (var y = 0; y < Height; y++)
        for (var x = 0; x < Width; x++)
        {
            if (Map[y, x] == CellType.Target)
            {
                var hasBoxes = Boxes.Any(b => b.X == x && b.Y == y);
                if (!hasBoxes) return;
            }
        }

        IsCompleted = true;
    }
}
