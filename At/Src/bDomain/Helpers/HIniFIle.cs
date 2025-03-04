using System.Runtime.InteropServices;
using System.Text;

namespace CO2.At.Src.bDomain.Helpers;

#pragma warning disable CA8604 // メソッドの結果を無視しない


public sealed class HIniFIle
{
    public HIniFIle(string iniPath)
    {
        //Todo4:カプセル化して、落とすケースと落とさないケース、およびデバッグログを出力できるクラスでカプセル化。環境が変わったときにいち早くファイルが存在しないことを検知して、解析にかかる時間を減らす。わざと落とす処理

        if (!File.Exists(iniPath))
        {
            throw new CustomBaseException($"iniファイルが存在しません ファイル名:{iniPath}");
        }

        IniPath = iniPath;
    }

    public string IniPath { get; } = string.Empty;

    public string GetIniValue(string section, string key)
    {
        StringBuilder sb = new (256);
        _ = GetPrivateProfileString(section, key, string.Empty, sb, sb.Capacity, IniPath);
        return sb.ToString();
    }

    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedstring, int nSize, string lpFileName);

}
#pragma warning restore CA8604 // メソッドの結果を無視しない

