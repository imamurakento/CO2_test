using Microsoft.Maui.Devices;


namespace CO2.At.Src.bDomain.Helpers;

internal class HFontSize
{

    public float GetFontSizeInPixels(float fontSizeInPoints)
    {
        // Windows の DPI を取得
        //float dpi = (float)DeviceDisplay.MainDisplayInfo.Density * 96; // 96は標準DPI
        float dpi = (float)1 * 96;
        return fontSizeInPoints * dpi / 72f;
    }

}
