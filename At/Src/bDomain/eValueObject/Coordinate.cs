using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.eValueObject;

internal class Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinate(int x, int y,bool isCenter)
    {
        X = x;
        Y = y;
    }
}
