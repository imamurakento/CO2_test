using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CO2.At.Src.bDomain.Helpers.HWindow;

namespace CO2.At.Src.bDomain.eObservableObject;

internal class CalcWidthPixel
{

    private static int ScreenWidth = 2560;

    private static int ScreenHeight = 1440;

    public int WidthRate { get; }

    public int HeightRate { get; }

    public int StartX { get; }

    public int StartY { get; }


    public CalcWidthPixel(double widthRate, double heightRate)
    {
        WidthRate = (int)(ScreenWidth * widthRate);
        HeightRate = (int)(ScreenWidth * heightRate);

        StartX = (ScreenWidth - WidthRate) / 2;
        StartY = (ScreenHeight - HeightRate) / 2;
    }
}
