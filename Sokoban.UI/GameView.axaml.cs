using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Sokoban.Logic;
using System.Linq;
using static Sokoban.Logic.Enums;

namespace Sokoban.UI;

public partial class GameView : UserControl
{
    private Game game;
    private Level currentLevel;
    public GameView(Level levelToLoad)
    {
        InitializeComponent();

        game = new Game(levelToLoad);
        currentLevel = levelToLoad;

        StartNewGame();
    }

    private void StartNewGame()
    {
        game = new Game(currentLevel);

        GameGrid.Rows = game.Height;
        GameGrid.Columns = game.Width;

        Draw();
    }

    private void Draw()
    {
        GameGrid.Children.Clear();

        if (StepsText != null)
            StepsText.Text = $"Steps: {game.Steps}";

        for (var y = 0; y < game.Height; y++)
        for (var x = 0; x < game.Width; x++)
        {
            var cellContainer = new Panel();

            var cellType = game.Map[y, x];
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

            var box = game.Boxes.FirstOrDefault(b => b.X == x && b.Y == y);
            if (box != null)
            {
                if (cellType == CellType.Target) objectSprite = "box_on_target.png";
                else objectSprite = "box.png";
            }

            if (game.Player.X == x && game.Player.Y == y)
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

            GameGrid.Children.Add(cellContainer);
        }

        if (game.IsCompleted && StatusText != null)
        {
            StatusText.Text = "U WON";
            StatusText.Foreground = Brushes.Green;
        }
    }

    public void HandleInput(Key key)
    {
        if (game == null) return;

        if (key == Key.R)
        {
            StartNewGame();
            return;
        }
        if (game.IsCompleted) return;

        switch (key)
        {
            case Key.Up: case Key.W: game.Move(Direction.Up); break;
            case Key.Down: case Key.S: game.Move(Direction.Down); break;
            case Key.Left: case Key.A: game.Move(Direction.Left); break;
            case Key.Right: case Key.D: game.Move(Direction.Right); break;
            case Key.R: StartNewGame(); break;
        }
        Draw();
    }
}