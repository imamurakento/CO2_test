using CO2.At.Src.bDomain.aLogics;

namespace CO2.At.Src.bDomain.Helpers;

#pragma warning disable CS8604

internal static class HRreources
{

    public static Color GetColor(string colorKey)
    {
        NullGuard.NullThrow(Application.Current, nameof(Application.Current));
        return Application.Current.Resources.TryGetValue(colorKey, out var color) && color is Color col ? col : null;
    }

    public static void SetColor(string colorKey, Color color)
    {
        NullGuard.NullThrow(Application.Current, nameof(Application.Current));

        if (Application.Current.Resources.TryGetValue(colorKey, out var existingColor) && existingColor is Color)
        {
            Application.Current.Resources[colorKey] = color;
            HLog.Info($"setColor: Updated color key '{colorKey}' with value {color}.");
        }
        else
        {
            HLog.Warn($"setColor: Color key '{colorKey}' not found.");
        }
    }

    //ToDo:値は取れているが、整形して取れていないので、まだ変更が必要
    public static string GetStringResource(string key_name)
    {
        if (Application.Current == null)
        {
            throw new ArgumentNullException($"{nameof(Application)}" + Def.NULLASCCESS);
        }

        if (Application.Current.Resources.TryGetValue(key_name, out var spaceValue))
        {
            if (spaceValue is string stringValue)
            {
                //// 文字列を整数に変換（エラーチェックは省略）
                if (int.TryParse(stringValue.Trim('"'), out int space))
                {
                    //// spaceを何かの設定に使用
                    HLog.Info($"GetStringResource:Space-1 value: {space}");
                }
                else
                {
                    HLog.Info("GetStringResource:Failed to parse Space-1 value.");
                }
            }
        }
        else
        {
            HLog.Info("GetStringResource:Space-1 key not found in resources.");
        }

        HLog.Info("GetStringResource:Result: KeyName:" + key_name + " KeyValue:" + (string)spaceValue);
        return (string)spaceValue;
    }

    public static bool SetStringResource(string key_name, string key_value)
    {
        if (Application.Current == null)
        {
            throw new ArgumentNullException($"{nameof(Application)}" + Def.NULLASCCESS);
        }

        //// リソースから値を取得し、新しい値で更新
        if (Application.Current.Resources.TryGetValue("Space-1", out var originalValue))
        {
            if (originalValue is string originalStringValue)
            {
                Application.Current.Resources[key_name] = key_value;

                HLog.Info($"GetStringResource:Space-1 original value: {originalStringValue.Trim('"')}");
                HLog.Info($"GetStringResource:Space-1 updated value: 16");
            }
        }
        else
        {
            HLog.Info("GetStringResource:Space-1 key not found in resources.");
        }

        HLog.Info("SetStringResource:Result: KeyName:" + key_name + " KeyValue:" + (string)key_value);
        return true;
    }
}

#pragma warning disable CS8604

