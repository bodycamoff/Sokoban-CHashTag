using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Logic;

public class Box : GameObject
{
    public Box(int x, int y) : base(x, y) { }
    public Box() : base(0,0) { }
}
