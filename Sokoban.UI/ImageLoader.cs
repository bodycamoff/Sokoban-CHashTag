using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;

namespace Sokoban.UI;

/// <summary>
/// Класс для загрузки ресурсов (пока только картинки)
/// Реализует кеширование чтобы избежать частого чтения одного файла
/// </summary>
public static class ImageLoader
{
    /// <summary>
    /// Храним в словаре "имя файла" : "Bitmanp(структура для хранения картинок в памяти)"
    /// </summary>
    private static Dictionary<string, Bitmap> cache = new Dictionary<string, Bitmap>();

    public static Bitmap Load(string name)
    {
        if (cache.ContainsKey(name)) return cache[name];

        var uri = new Uri($"avares://Sokoban.UI/Assets/{name}");
        var bitmap = new Bitmap(AssetLoader.Open(uri));
        cache[name] = bitmap;
        return bitmap;
    }
}