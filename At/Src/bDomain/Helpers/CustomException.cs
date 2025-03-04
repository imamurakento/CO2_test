namespace CO2.At.Src.bDomain.Helpers;

public class CustomBaseException : Exception
{
    public CustomBaseException(string optionMsg = "")
        : base($"アプリ内部エラー発生:{optionMsg}")
    {
        string logReportMsg = $"アプリ内部エラー発生:{optionMsg}";
        HLog.Err(logReportMsg);
    }
}

public class CustomNullException : Exception
{
    public CustomNullException(string optionMsg = "")
        : base($"NULLアクセスエラー発生:{optionMsg}")
    {
        string logReportMsg = $"NULLアクセスエラー発生:{optionMsg}";
        HLog.Err(logReportMsg);
    }
}

public class CustomDbSQLException : Exception
{
    public CustomDbSQLException(string optionMsg = "")
        : base($"データベース:SQL発行:内部エラー発生:{optionMsg}")
    {
        string logReportMsg = $"データベース:SQL発行:内部エラー発生:{optionMsg}";
        HLog.Err(logReportMsg);
    }
}

public class CustomValueObjectException : Exception
{
    public CustomValueObjectException(string typeName, string errMsg, string failValue)
        : base(typeName)
    {
        string logReportMsg = $"ValueObject：データベースからの取得に失敗しました。データ形式が正しくありません。形式  {typeName} エラー内容: {errMsg} 不正な値:{failValue}";
        HLog.Err(logReportMsg);
    }
}

public class CustomNoFindException : Exception
{
    public CustomNoFindException(string message = "顧客インスタンスにアクセスできませんでした。")
        : base(message)
    {
        HLog.Err("カスタム例外発生:" + message);
    }
}