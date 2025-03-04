using System.Diagnostics;

namespace CO2.At.Src.bDomain.Helpers;

public class StopWatch(string message = "")
{
    private readonly string _msg = message;
    private readonly Stopwatch _sw = new ();

    public void Start(string message = "")
    {
        _sw.Start();
    }

    public void Stop()
    {
        _sw.Stop();
    }

    public void StopAndShowTime()
    {
        _sw.Stop();
        ShowTime();
    }

    public void ShowTime()
    {
        string msgTmp = $"計測結果:{_msg}:経過時間:　{_sw.ElapsedMilliseconds}ミリ秒";
        HLog.Dbg(msgTmp);
        Debug.WriteLine(msgTmp);

    }
}
