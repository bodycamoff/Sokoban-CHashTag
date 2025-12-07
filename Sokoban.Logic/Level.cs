using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Logic;

public class Level
{
    public string Name { get; set; } = "New Level";

    public int Heigth { get; set; }
    public int Width { get; set; }

    public int[] MapLayout { get; set; }

    public int PlayerStartX { get; set; }
    public int PlayerStartY { get; set; }

    public List<Box> InitialBoxes { get; } = new();

    public bool IsCompleted { get; set; }
    public int BestSteps { get; set; } = 0;
}


