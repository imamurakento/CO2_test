using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.bDomain.eValueObject;

namespace CO2.At.Src.bDomain.Helpers;

internal class HWindowSize
{
    ContentPageSize pageSize;

    public int Width { get; }
    public int Height { get; }

    public HWindowSize(int width, int height)
    {
        Width = width;
        Height = height;
    }
}
