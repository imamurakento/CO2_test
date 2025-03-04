using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using log4net;
using log4net.Config;

namespace CO2.At.Src.bDomain.Helpers;

#pragma warning disable SA1500
#pragma warning disable CS8602


public static class HLog
{
    private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);


    public static void InitializeLog()
    {
        //ToDo:要チェック箇所
        XmlConfigurator.Configure();             ////App.configの読み込み
    }

    public static void Dbg(string message, [CallerMemberName] string callingMethod = "", [CallerFilePath] string callingFile = "", [CallerLineNumber] int callingLineNumber = 0)
    {
        string logMessage = $" {callingMethod} : {message} ";
        _logger.Debug(logMessage);
        Console.WriteLine($"メソッド名: {callingMethod} file: {callingFile} line: {callingLineNumber} メッセージ: {message}");
    }

    public static void Info(string message)
    {
        _logger.Info(message);
        Console.WriteLine(message);
    }
    public static void Warn(string message)
    {
        _logger.Warn(message);
        Console.WriteLine(message);
    }

    public static void Err(string message)
    {
        _logger.Error(message);
        Console.WriteLine(message);
    }
    public static void Fatal(string message)
    {
        _logger.Fatal(message);
        Console.WriteLine(message);
    }

    public static void Except(Exception ex, string? message = null, [CallerMemberName] string callingMethod = "", [CallerFilePath] string callingFile = "", [CallerLineNumber] int callingLineNumber = 0)
    {

        string logMessage = !string.IsNullOrEmpty(message) ? "  カスタムメッセージ:" + message : string.Empty;     //// 三項演算子を使用して、message引数にデータがあるときのみ、"カスタムメッセージ:"を付加する。

        _logger.Error("=== 例外発生 出力開始 ===");
        _logger.Error($"関数名1: {callingMethod} ファイル: {callingFile} 行: {callingLineNumber}");
        _logger.Error($"関数名2: {ex.Source}");

        ////_logger.Error($"例外: メソッド型名: {ex.TargetSite}");
        _logger.Error($"例外: {GetCurrentMethodName()}");
        ////_logger.Error($"例外: 内容：{ex.Message}");
        _logger.Error($"例外: カスタムメッセージ：{logMessage}");

        _logger.Error("=== 例外種別 出力開始 ===");
        ExceptionHandler.LogExceptionDetails(ex);
        _logger.Error("=== 例外種別 出力終了 ===");

        _logger.Error("=== 例外 トレース情報 出力開始 ===");
        _logger.Error(ex.StackTrace);
        _logger.Error("=== 例外 トレース情報 出力終了 ===");

        _logger.Error("=== 例外発生 出力終了 ===");
        //GetStackTraceInfo();

    }

    public static string GetStackTraceInfo()
    {
        StringBuilder result = new ();

        StackTrace st = new (true);

        StackFrame[] frames = st.GetFrames();

        result.AppendLine("----------");

        // 最初のStackFrame は GetStackTraceInfo 自身なので除外する

        for (int i = 1; i < frames.Length; i++)
        {
            StackFrame sf = frames[i];

            result.AppendFormat("ファイル:{0},行:{1},列:{2}, メソッド名:{3}.{4}{5}", sf.GetFileName(), sf.GetFileLineNumber(), sf.GetFileColumnNumber(), sf.GetMethod().DeclaringType, sf.GetMethod().Name, Environment.NewLine);

            result.AppendLine("----------");
        }

        HLog.Info("GetStackTraceInfo:" + result.ToString());

        return result.ToString();

    }

    private static string GetCurrentMethodName()
    {
        var stackTrace = new StackTrace();
        var frame = stackTrace.GetFrame(2); // 2番目のフレームを取得（呼び出し元のメソッド）
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
        var method = frame.GetMethod();

        string makMsg = $"クラス名:{method.DeclaringType.Name} メソッド名: {method.Name}";
        return makMsg;
    }

}

#pragma warning disable SA1500
#pragma warning disable CS8602

