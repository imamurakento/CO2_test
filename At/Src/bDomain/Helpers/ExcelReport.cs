using System.Runtime.InteropServices;

namespace CO2.At.Src.bDomain.Helpers;

public class ExcelReport
{

    private readonly string? _iniPath;

    public ExcelReport(string? filePath)
    {
        _iniPath = filePath;
        ResetErr();
    }

    public string? ErrMsg { get; set; }

    public string CsvPath { get; set; }

    private string? MsgFromClass { get; set; }

    private string? ExcelPath { get; set; }

    private string? MacroName { get; set; }

    private string? PrintPath { get; set; }



    //public bool Reflect_IniFile()
    //{
    //    string errMsg = MsgFromClass + " ファイルアクセス処理に失敗しました。:要因：";

    //    try
    //    {
    //        if (!File.Exists(_iniPath))
    //        {
    //            ErrMsg = errMsg + " iniファイルが存在しません ファイル名:" + _iniPath;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        var parser = new FileIniDataParser();
    //        IniData data = parser.ReadFile(_iniPath);

    //        string excelFilePath = data["CSVToExcel"]["ExcelMacroFilePath"];

    //        if (string.IsNullOrEmpty(excelFilePath))
    //        {
    //            ErrMsg = errMsg + "エクセルファイルがiniファイルに定義されていません ファイル名:" + excelFilePath;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        if (!File.Exists(excelFilePath))
    //        {
    //            ErrMsg = errMsg + "エクセルファイルが存在しません ファイル名:" + excelFilePath;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        if (Path.GetExtension(excelFilePath).ToLower() != ".xlsm")
    //        {
    //            ErrMsg = errMsg + "エクセルファイルがマクロファイルではありません。:" + excelFilePath;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        string macroName = data["CSVToExcel"]["MacroName"];
    //        if (string.IsNullOrEmpty(macroName))
    //        {
    //            ErrMsg = errMsg + "マクロがiniファイルに定義されていません。:" + macroName;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        string csvFile = data["CSVToExcel"]["CsvFilePath"];
    //        if (string.IsNullOrEmpty(csvFile))
    //        {
    //            ErrMsg = errMsg + "CSVファイルがiniファイルに定義されていません。:" + csvFile;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        if (File.Exists(csvFile))
    //        {
    //            File.Delete(csvFile);
    //        }

    //        using (FileStream fs = File.Create(csvFile))
    //        {
    //            // The file is created, we can close the stream without writing anything
    //        }

    //        string printPath = data["CSVToExcel"]["ExcelOutFilePath"];

    //        if (!File.Exists(printPath))
    //        {
    //            ErrMsg = errMsg + "エクセルファイルが存在しません ファイル名:" + printPath;
    //            HLog.Fatal(ErrMsg);
    //            return false;
    //        }

    //        ExcelPath = excelFilePath;

    //        MacroName = macroName;

    //        CsvPath = csvFile;

    //        PrintPath = printPath;

    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrMsg = "CSVファイルへのアクセスに失敗しました。";
    //        HLog.Except(ex, MsgFromClass);
    //        return false;
    //    }
    //}

    public void ResetErr()
    {
        ErrMsg = string.Empty;
        ErrMsg = string.Empty;
        CsvPath = string.Empty;
        ExcelPath = string.Empty;
        MacroName = string.Empty;
        PrintPath = string.Empty;
    }

    public bool RunExcelMacro()
    {
        string errMsg = MsgFromClass + " エクセルマクロの起動に失敗しました。:要因：";

        try
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbooks = excelApp.Workbooks;
            var workbook = workbooks.Open(ExcelPath);

            try
            {
                excelApp.Run(MacroName);
            }
            catch (Exception ex)
            {
                ErrMsg = errMsg + "エクセルマクロの起動で例外発1。";
                HLog.Except(ex, ErrMsg);
                return false;
            }
            finally
            {
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }

            return true;
        }
        catch (Exception ex)
        {
            ErrMsg = errMsg + "エクセルマクロの起動で例外発生2。";
            HLog.Except(ex, ErrMsg);
            return false;
        }
    }

    public bool RunExcelPrint()
    {
        string errMsg = MsgFromClass + " エクセル帳票の印刷に失敗しました。:要因：";

        try
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbooks = excelApp.Workbooks;
            var workbook = workbooks.Open(PrintPath);

            try
            {
                //// アクティブシートを取得
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

                //// 印刷設定（必要に応じて変更可能）
                //                worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                //                worksheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;

                //// 印刷プレビューを表示（オプション）
                //// worksheet.PrintPreview();

                //// 印刷を実行
                worksheet.PrintOut();
            }
            catch (Exception ex)
            {
                ErrMsg = errMsg + "例外発生1";
                HLog.Except(ex, ErrMsg);
                return false;
            }
            finally
            {
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }

            return true;
        }
        catch (Exception ex)
        {
            ErrMsg = errMsg + "例外発生2";
            HLog.Except(ex, ErrMsg);
            return false;
        }
    }
}
