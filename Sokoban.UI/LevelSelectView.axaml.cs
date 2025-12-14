using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Sokoban.Logic;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

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

            var btn = new Button
            {
                Content = fileName,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Height = 40,
                FontSize = 18
            };

            btn.Click += (s, e) => OnLevelSelectd?.Invoke(file);

            LevelsContainer.Children.Add(btn);
        }
    }

}