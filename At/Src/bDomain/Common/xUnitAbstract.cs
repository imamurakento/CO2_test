using System.Reflection;
using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.bDomain.Common;

internal abstract class XUnitAbstract
{

    protected string _className;

    public void ExecuteAllMethod()
    {
        //MethodInfo[] methodInfos = typeof(XUnitValueObject).GetMethods(BindingFlags.Public | BindingFlags.Instance);
        MethodInfo[] methodInfos = GetType().GetMethods();


        _className = GetType().Name;
        HLog.Info("typeInfo Class:Name: " + GetType().Name);


        // メソッド名をデバッグ出力して、すべてのメソッドを呼び出す
        foreach (var method in methodInfos)
        {
            HLog.Info($"クラス名:" + _className + " メソッド名: { method.Name}" + "単体テスト開始");

            if (method.Name == "ExecuteAllMethod")
            {
                continue;
            }

            try
            {
                // メソッドのパラメータを取得
                var parameters = method.GetParameters();

                // パラメータの数に応じてメソッドを呼び出す
                if (parameters.Length == 0)
                {
                    // 引数なしのメソッドを呼び出す
                    method.Invoke(this, null);
                }
                //else if (parameters.Length == 2 && parameters[0].ParameterType == typeof(int) && parameters[1].ParameterType == typeof(int))
                //{
                //    // 引数が2つのメソッド（Addメソッド）を呼び出す
                //    object result = method.Invoke(obj, new object[] { 5, 3 });
                //    Console.WriteLine($"Result of {method.Name}: {result}");
                //}

                HLog.Info($"クラス名:" + _className + " メソッド名: { method.Name}" + "単体テスト結果:OK");
            }
            catch (Exception ex)
            {
                HLog.Info($"クラス名:" + _className + " メソッド名: { method.Name}" + "単体テスト結果:NG");
                HLog.Except(ex);

                //ToDo:エラー発生時にフラグをもうけ、停止フラグONなら、ここで止める処理をいれる。
            }

        }

    }






}
