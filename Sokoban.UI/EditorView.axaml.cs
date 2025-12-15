using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Sokoban.Logic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Sokoban.Logic.Enums;

namespace Sokoban.UI;

public partial class EditorView : UserControl
{
    private int width = 10;
    private int height = 10;

    private CellType[,] map;
    private List<Box> boxes = new List<Box>();
    private Player player = new Player(0, 0);

    public EditorView()
    {
        InitializeComponent();
        InitializeNewMap();
        BtnSave.Click += (s, e) => SaveLevel();
        Draw();
    }

    private void SaveLevel()
    {
        var levelName = TbLevelName.Text;
        if (string.IsNullOrEmpty(levelName))
            levelName = "unknown_level";

        var flatMap = new int[width * height];

        for (var y = 0; y < height; y++)        
        for (var x = 0; x < width; x++)
        {
            var idx = y * width + x;
            flatMap[idx] = (int)map[y, x];
        }

        var levelToSave = new Level
        {
            Name = levelName,
            Width = width,
            Heigth = height,
            MapLayout = flatMap,
            PlayerStartX = player.X,
            PlayerStartY = player.Y,

            InitialBoxes = boxes.Select(b => new Box(b.X, b.Y)).ToList()
        };

        var folderPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Levels");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fullPath = System.IO.Path.Combine(folderPath, $"{levelName}.json");

        var service = new LevelService();
        service.SaveLevel(levelToSave, fullPath);

        BtnSave.Content = $"Сохранено в {fullPath}!";
    }

    private void InitializeNewMap()
    {
        map = new CellType[height, width];
        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            map[y, x] = CellType.Empty; 

        player.X = 0; player.Y = 0;
    }

    private void OnCellClicked(int x, int y)
    {
        if (RbWall.IsChecked == true)
        {
            RemoveObjectsAt(x, y);
            map[y, x] = CellType.Wall;
        }
        else if (RbFloor.IsChecked == true)
        {
            RemoveObjectsAt(x, y);
            map[y, x] = CellType.Empty;
        }
        else if (RbTarget.IsChecked == true)
        {
            RemoveObjectsAt(x, y);
            map[y, x] = CellType.Target;
        }
        else if (RbBox.IsChecked == true)
        {
            
            if (map[y, x] == CellType.Wall) map[y, x] = CellType.Empty;
            
            if (!boxes.Any(b => b.X == x && b.Y == y))
                boxes.Add(new Box(x, y));
        }
        else if (RbPlayer.IsChecked == true)
        {
            
            if (map[y, x] == CellType.Wall) map[y, x] = CellType.Empty;
            player.X = x;
            player.Y = y;
            
            boxes.RemoveAll(b => b.X == x && b.Y == y);
        }

        Draw(); 
    }

    private void OnGridClicked(object sender, PointerPressedEventArgs e) { }

    private void RemoveObjectsAt(int x, int y) => boxes.RemoveAll(b => b.X == x && b.Y == y);

    private void Draw()
    {
        EditorGrid.Children.Clear();
        EditorGrid.Rows = height;
        EditorGrid.Columns = width;

        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
        {
            var cellContainer = new Panel();

            var cellType = map[y, x];
            var bgSprite = "floor.png";

            if (cellType == CellType.Wall) bgSprite = "wall.png";
            else if (cellType == CellType.Target) bgSprite = "target.png";

            var bgImg = new Image
            {
                Stretch = Stretch.Uniform,
                Source = ImageLoader.Load(bgSprite)
            };
            cellContainer.Children.Add(bgImg);

            string objectSprite = null;

            var box = boxes.FirstOrDefault(b => b.X == x && b.Y == y);
            if (box != null)
            {
                if (cellType == CellType.Target) objectSprite = "box_on_target.png";
                else objectSprite = "box.png";
            }

            if (player.X == x && player.Y == y)
                objectSprite = "player.png";
            
            if (objectSprite != null)
            {
                Image objImg = new Image
                {
                    Stretch = Stretch.Uniform,
                    Source = ImageLoader.Load(objectSprite)
                };
                cellContainer.Children.Add(objImg);
            }

            var capturedX = x;
            var capturedY = y;

            cellContainer.PointerPressed += (s, e) => OnCellClicked(capturedX, capturedY);

            EditorGrid.Children.Add(cellContainer);
        }
    }
}
