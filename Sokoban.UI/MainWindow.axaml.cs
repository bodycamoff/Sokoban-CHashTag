using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Sokoban.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using static Sokoban.Logic.Enums;
namespace Sokoban.UI;

public partial class MainWindow : Window
{
    private MenuView menuView;
    private GameView gameView;

    public MainWindow()
    {
        InitializeComponent();

        menuView = new MenuView();

        menuView.BtnPlay.Click += (s, e) => StartGame();
        menuView.BtnEditor.Click += (s, e) => OpenEditor();
        menuView.BtnExit.Click += (s, e) => Close();

        MainContent.Content = menuView;
    }

    private void OpenEditor()
    {
        var editorView = new EditorView();

        editorView.BtnBack.Click += (s, e) => MainContent.Content = menuView;

        MainContent.Content = editorView;
    }

    private void StartGame()
    {
        gameView = new GameView();
        MainContent.Content = gameView;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (MainContent.Content == gameView)
        {
            gameView.HandleInput(e.Key);
        }
    }
}