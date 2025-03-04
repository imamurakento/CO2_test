using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class Birthday : InputValidatorBase, IInputValidator
{

    [ObservableProperty]
    private ObservableCollection<string>? _eraList;

    [ObservableProperty]
    private string _selectedEra;

    [ObservableProperty]
    private string _year = string.Empty;

    [ObservableProperty]
    private string _month = string.Empty;

    [ObservableProperty]
    private string _day = string.Empty;

    public Birthday()
    {
        EraList =[JapaneseEra.令和.ToString(),
                  JapaneseEra.平成.ToString(),
                  JapaneseEra.昭和.ToString(),
                  JapaneseEra.大正.ToString(),
                  JapaneseEra.明治.ToString()];
        SelectedEra = JapaneseEra.昭和.ToString();
    }
    public enum JapaneseEra : int
    {
        令和,
        平成,
        昭和,
        大正,
        明治,
    }

    public void SetBirthday(DateTime fullValue)
    {

        CultureInfo culture = new CultureInfo("ja-JP");
        string strDate = fullValue.ToString("yyyy/MM/dd", culture);

        var match = Regex.Match(strDate, @"^(?<year>\d{4})/(?<month>\d{2})/(?<day>\d{2})$");
        if (!match.Success)
        {
            //DbgPrint();
            SetError(nameof(Birthday), "生年月日の形式が不正です。例: 2024/05/08", strDate);
        }

        ////西暦から和暦に変換
        Year = GetJapaneseCalender(match.Groups["year"].Value, match.Groups["month"].Value, match.Groups["day"].Value)[1];
        SelectedEra = GetJapaneseCalender(match.Groups["year"].Value, match.Groups["month"].Value, match.Groups["day"].Value)[0];
        Month = match.Groups["month"].Value;
        Day = match.Groups["day"].Value;

        if (!IsValidated())
        {
            SetError(nameof(Birthday), "生年月日の形式が不正です。例: 2024/05/08", strDate);
        }


    }

    public string GetBirthday()
    {

        Year = GetWesternCalender(Year, Month, Day);
        return Year + "/" + Month + "/" + Day;
    }

    public bool IsValidated()
    {
        string westYear = GetWesternCalender(Year, Month, Day);

        string chkDate = westYear + "/" + Month + "/" + Day;
        return InputValidator.IsValidDate(chkDate);
    }

    private static string[] GetJapaneseCalender(string year, string month, string day)
    {
        CultureInfo jpCulture = new ("ja-JP", true);
        jpCulture.DateTimeFormat.Calendar = new JapaneseCalendar();
        int yearTmp = int.Parse(year);
        int monthTmp = int.Parse(month);
        int dayTmp = int.Parse(day);
        DateTime seDate = new (yearTmp, monthTmp, dayTmp);
        return [seDate.ToString("gg", jpCulture), seDate.ToString("ggy", jpCulture)];
    }

    private static string GetWesternCalender(string japanYear, string month, string day)
    {
        CultureInfo jpCulture = new ("ja-JP", true);
        jpCulture.DateTimeFormat.Calendar = new JapaneseCalendar();
        DateTime waDate = DateTime.Parse(japanYear + "年" + month + "月" + day + "日", jpCulture);
        return waDate.ToString("yyyy");
    }

    //private void DbgPrint()
    //{
    //    HLog.Dbg("=== VOBirthday インスタンス情報出力 ===");
    //    HLog.Dbg($"_year: {_year} Year: {Year}");
    //    HLog.Dbg($"_month:{_month} Month: {Month}");
    //    HLog.Dbg($"day:{_day} Day:{Day}");
    //    HLog.Dbg($"_selectedEra: {_selectedEra} SelectedEra: {SelectedEra}");
    //}
}
