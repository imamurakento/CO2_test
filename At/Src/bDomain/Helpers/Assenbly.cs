using System.Reflection;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CO2.At.Src.bDomain.Helpers;

public static class Assenbly
{

    public static void DispCo2Version()
    {
        string title = AppInfo.Current.VersionString;
        Assembly assembly = Assembly.GetExecutingAssembly();
        var executingAssembly = Assembly.GetExecutingAssembly();

        HLog.Info("=== " + AppInfo.Current.Name + " バージョン:" + assembly.GetName().Version + "===");

#if false
        ////アセンブリ情報

        HLog.Info("========= アセンブリ情報出力 =========");

        HLog.Info("AppInfo:title: " + AppInfo.Current.VersionString);

        HLog.Info("AppInfo:Name: " + AppInfo.Current.Name);
        HLog.Info("Assembly.Name: " + assembly.GetName().Name);

        HLog.Info("AppInfo:Version: " + AppInfo.Current.VersionString);
        HLog.Info("assembly.Version: " + assembly.GetName().Version);
        AssemblyName name = executingAssembly.GetName();
        HLog.Info("GetExecutingAssembly.Version:" + name.Version);

        HLog.Info("AppInfo:PackageName: " + AppInfo.Current.PackageName);
        HLog.Info("マニフェスト: " + assembly.FullName);

        var location = Assembly.GetExecutingAssembly().Location;
        HLog.Info("========= パス情報出力 =========");
        HLog.Info("Assembly.Location:" + Path.GetDirectoryName(location));
        HLog.Info("AppContext.BaseDirectory:" + AppContext.BaseDirectory);

        HLog.Info("========= CustomAttributes情報出力 =========");

        foreach (var ca in executingAssembly.CustomAttributes
            .Where(c => c.AttributeType.Name.StartsWith("Assembly"))
            .Select(c => new
            {
                Name = c.AttributeType.Name
               .Replace("Assembly", string.Empty)
               .Replace("Attribute", string.Empty),

                Value = c.ConstructorArguments[0],

            }))
        {
            HLog.Info("CustomAttributes情報:" + ca.Name + ':' + ca.Value);
        }
#endif
    }

}
