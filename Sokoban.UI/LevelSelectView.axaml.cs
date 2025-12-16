using Avalonia.Controls;
using Avalonia.Media;
using Sokoban.Logic;
using System;
using System.IO;

namespace Sokoban.UI;

public partial class LevelSelectView : UserControl
{
    public event Action<string> OnLevelSelectd;
    public Button BackButton => BtnBack;
    public LevelSelectView()
    {
        InitializeComponent();
        LoadLevelList();
    }

    private void LoadLevelList()
    {
        var folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Levels");
        if (!Directory.Exists(folderPath))
        {
            var txt = new TextBlock
            {
                Text = "Уровни не найдены",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
            };

            LevelsContainer.Children.Add(txt);
            return;
        }

        var files = Directory.GetFiles(folderPath, "*.json");

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var levelData = new LevelService().LoadLevel(file);

            var btnText = fileName;
            if (levelData.IsCompleted) 
                btnText += $" (Best Steps : {levelData.BestSteps})";

            var btnColor = levelData.IsCompleted
                ? Brush.Parse("#2e7d32")
                : Brush.Parse("#2D2D30");

            var btn = new Button
            {
                Content = btnText,
                Foreground = Brushes.White,           
                Background = btnColor,  
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Height = 45,
                FontSize = 18
            };

            btn.Click += (s, e) => OnLevelSelectd?.Invoke(file);

            LevelsContainer.Children.Add(btn);
        }
    }
}