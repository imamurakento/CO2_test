using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.eValueObject;

internal class ContentPageSize
{
    public int Width { get; }
    public int Height { get; }

    public ContentPageSize(int width,int height)
    {
        Width = width;
        Height = height;
    }
}
