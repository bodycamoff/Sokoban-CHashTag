namespace Sokoban.Logic;

/// <summary>
/// Абстрактный класс для всех игровых объектов, имеющих координаты на карте
/// Реализует общий функционал хранения позиции
/// </summary>
public abstract class GameObject
{
    public int X { get; set; }
    public int Y {  get; set; }

    public GameObject(int x, int y)
    {
        X = x;
        Y = y;
    }
    /// <summary>
    /// Пустой конструктор нужен для сериализации из JSON
    /// </summary>
    public GameObject() { }
}
