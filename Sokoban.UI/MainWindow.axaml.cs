using Avalonia.Controls;
using Avalonia.Input;
using Sokoban.Logic;

namespace Sokoban.UI;

/// <summary>
/// Главное окно приложения
/// Меняет содержимое (content) между меню, игрой и редактором увроня
/// </summary>
public partial class MainWindow : Window
{
    private MenuView menuView;
    private GameView gameView;

    public MainWindow()
    {
        InitializeComponent();

        menuView = new MenuView();

        menuView.BtnPlay.Click += (s, e) => OpenLevelSelect();
        menuView.BtnEditor.Click += (s, e) => OpenEditor();
        menuView.BtnExit.Click += (s, e) => Close();

        MainContent.Content = menuView;
    }

    /// <summary>
    /// Переключает на экран выбора уровня
    /// Подписывается на событие выбора файла чтобы запустить игру
    /// </summary>
    private void OpenLevelSelect()
    {
        var selectView = new LevelSelectView();

        selectView.BackButton.Click += (s, e) => MainContent.Content = menuView;

        // вызовется когда selectView сработает делегат OnLevelSelectd
        selectView.OnLevelSelectd += (filePath) => StartGame(filePath);

        MainContent.Content = selectView;
    }

    private void OpenEditor()
    {
        var editorView = new EditorView();

        editorView.BtnBack.Click += (s, e) => MainContent.Content = menuView;

        MainContent.Content = editorView;
    }

    private void StartGame(string filePath)
    {
        var service = new LevelService();
        var loadLevel = service.LoadLevel(filePath);
        
        if (loadLevel == null) return;

        gameView = new GameView(loadLevel);
        MainContent.Content = gameView;
        this.Focus();
    }

    /// <summary>
    /// Перехватывает нажатие клавиш
    /// Благодаря методу, например Escape работает всегда а стрелки только во время игры на уровне
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
            if (MainContent.Content != menuView)
            {
                MainContent.Content = menuView;
                return;
            }

        if (MainContent.Content == gameView)        
            gameView.HandleInput(e.Key); 
    }
}