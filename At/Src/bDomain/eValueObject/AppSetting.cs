using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.bDomain.eValueObject;

public sealed class AppSetting
{

    public bool IsDebug { get; }
    public bool IsDbUse { get; }

    public bool IsMigrationMode { get; }

    public bool IsShellMode { get; }

    private readonly HIniFIle _iniFile;

    public AppSetting(HIniFIle iniFIle)
    {
        try
        {
            _iniFile = iniFIle;

            IsDebug = Convert.ToBoolean(_iniFile.GetIniValue("Env", "IsDebug"));

            IsDbUse = Convert.ToBoolean(_iniFile.GetIniValue("DataBase", "IsDbUse"));
            IsMigrationMode = Convert.ToBoolean(_iniFile.GetIniValue("DataBase", "IsMigrationMode"));

            IsShellMode = Convert.ToBoolean(_iniFile.GetIniValue("Window", "IsShellMode"));

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("INIファイルのデータを正しく読み込めませんでした。", ex);
        }
    }
}

