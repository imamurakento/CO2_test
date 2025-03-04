using System.Net;
using System.Text.RegularExpressions;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class Address : InputValidatorBase, IInputValidator
{
    private static readonly HttpClient HttpClient = new ();

    [ObservableProperty]
    private string _areaCode = string.Empty;
    [ObservableProperty]
    private string _townCode = string.Empty;
    [ObservableProperty]
    private string _location = string.Empty;

    public void SetAddress(string location)
    {
        Location = location;
    }
    public string GetAddress()
    {
        return Location;
    }

    public void SetPostalCode(string fullValue)
    {
        if (string.IsNullOrWhiteSpace(fullValue))
        {
            AreaCode = string.Empty;
            TownCode = string.Empty;
        }

        // 正規表現で郵便番号の形式をチェックし、分割
        var match = Regex.Match(fullValue, @"^(?<areaCode>\d{3})-(?<localCode>\d{4})$");
        if (!match.Success)
        {
            SetError(nameof(Address), "郵便番号の形式が不正です。例: 123-4567", fullValue);
        }

        AreaCode = match.Groups["areaCode"].Value;
        TownCode = match.Groups["localCode"].Value;

    }

    public string GetPostalCode()
    {
        return AreaCode + "-" + TownCode;
    }

    [RelayCommand]
    private async Task SearchAddress()
    {
        string postCode = $"{AreaCode}-{TownCode}";
        if (string.IsNullOrEmpty(postCode))
        {
            SetError(nameof(Address), "エラー: 郵便番号を入力してください。", postCode);
            return;
        }

        try
        {
            // ZipCloud API エンドポイント
            string apiUrl = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={postCode}";

            // APIにリクエストを送信
            HttpResponseMessage response = await HttpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            // レスポンスをJSONとして読み込み
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            // 成功した場合、住所情報を取得
            if (json["status"]?.ToString() == "200")
            {
                var addressInfo = json["results"]?[0];
                if (addressInfo != null)
                {
                    Location = $"{addressInfo["address1"]}{addressInfo["address2"]}{addressInfo["address3"]}";
                    return;
                }
                else
                {
                    SetError(nameof(Address), "エラー: 結果が見つかりません。", postCode);
                    return;
                }
            }
            else
            {
                SetError(nameof(Address), "エラー: 住所が見つかりません。", postCode);
                return;
            }
        }
        catch (Exception ex)
        {
            SetError(nameof(Address), $"エラー: 住所の検索中にエラーが発生しました。詳細: {ex.Message}", postCode);
        }
    }

    public bool IsValidated()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void ChkPostArea(string areaCode)
    {
        if (Regex.IsMatch(areaCode, "^[0-9]{3}$") && areaCode.Length == 3)
        {
            SetNormal();
            return;
        }
        else
        {
            SetError(nameof(Address), "郵便番号のエリアコードは数字3桁の形式で入力してください", areaCode);
            return;
        }
    }

    [RelayCommand]
    private void ChkPostTown(string areaCode)
    {
        if (Regex.IsMatch(areaCode, "^[0-9]{4}$") && areaCode.Length == 4)
        {
            SetNormal();
        }
        else
        {
            SetError(nameof(Address), "郵便番号のタウンコードは数字4桁の形式で入力してください", areaCode);
            return;
        }
    }


    //public VOPostalCode2(string areaCode, string townCode)
    //    {
    //    ErrorCode = Validate(areaCode, townCode);

    //    this.areaCode = areaCode;
    //    this.townCode = townCode;
    //    }
    //public VOPostalCode2(string fullCode)
    //    {
    //    if (string.IsNullOrWhiteSpace(fullCode))
    //        {
    //        throw new ArgumentException("郵便番号が空です。");
    //        }

    //    // 正規表現で郵便番号の形式をチェックし、分割
    //    var match = Regex.Match(fullCode, @"^(?<areaCode>\d{3})-(?<localCode>\d{4})$");
    //    if (!match.Success)
    //        {
    //        throw new ArgumentException("郵便番号の形式が不正です。例: 123-4567");
    //        }

    //    areaCode = match.Groups["areaCode"].Value;
    //    townCode = match.Groups["localCode"].Value;
    //    }

    //public string FullCode => $"{areaCode}-{townCode}";
    //public string AreaCode => $"{areaCode}";
    //public string TownCode => $"{townCode}";
    //public string ErrorCode { get; private set; } = string.Empty;

    //public VOPostalCode()
    //    {
    //    ValidateAreaCommand = new RelayCommand(ValidateInput);
    //    }

    //public RelayCommand ValidateAreaCommand { get; }
    //public bool IsValidAreaError=false;

    //public static ValidationResult ValidateInpuArea(string value, ValidationContext context)
    //    {
    //    if (string.IsNullOrWhiteSpace(value))
    //        {
    //        return new ValidationResult("入力は必須です。");
    //        }

    //    if (Regex.IsMatch(value, "^[0-9]{3}$") && value.Length == 3)
    //        {
    //        return new ValidationResult("3桁の数字で入力してください。");
    //        }

    //    return ValidationResult.Success;
    //    }



    //private void ValidateInput()
    //    {
    //    // ValidationContext を作成
    //    var validationContext = new ValidationContext(this)
    //        {
    //        MemberName = nameof(AreaCode)
    //        };

    //    // ValidateInput関数を呼び出し
    //    var validationResult = ValidateInpuArea(AreaCode, validationContext);

    //    // 結果に応じてエラーメッセージを設定
    //    if (validationResult != ValidationResult.Success)
    //        {

    //        ErrorMsg = validationResult.ErrorMessage;
    //        }
    //    else
    //        {
    //        ErrorMsg = string.Empty; // エラーメッセージをクリア
    //        }
    //    }

}
