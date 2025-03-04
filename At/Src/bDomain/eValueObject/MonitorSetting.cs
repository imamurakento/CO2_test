using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.bDomain.eValueObject;

internal class MonitorSetting
{
    private readonly HIniFIle _iniFile;

    public int ResolutionWidth { get; }
    public int ResolutionHeight { get; }

    public double CentimetersWidth { get; }
    public double CentimetersHeight { get; }

    public double CorrectionRate { get; }

    public MonitorSetting(HIniFIle iniFIle)
    {
        try
        {
            _iniFile = iniFIle;

            ////////////////////////////////////////////////////
            ////[ScreenSize]セクション
            ////////////////////////////////////////////////////
            ResolutionWidth = Convert.ToInt32(iniFIle.GetIniValue("ScreenSize", "ResolutionWidth"));
            ResolutionHeight = Convert.ToInt32(iniFIle.GetIniValue("ScreenSize", "ResolutionHeight"));

            CentimetersWidth = Convert.ToDouble(iniFIle.GetIniValue("ScreenSize", "CentimetersWidth"));
            CentimetersHeight = Convert.ToDouble(iniFIle.GetIniValue("ScreenSize", "CentimetersHeight"));

            CorrectionRate = Convert.ToDouble(iniFIle.GetIniValue("ScreenSize", "CorrectionRate"));
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("INIファイルのデータを正しく読み込めませんでした。", ex);
        }
    }
}
