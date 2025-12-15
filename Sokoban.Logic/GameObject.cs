namespace Sokoban.Logic;

public abstract class GameObject
{
    public int X { get; set; }
    public int Y {  get; set; }

    public GameObject(int x, int y)
    {
        X = x;
        Y = y;
    }

    public GameObject() { }
}
