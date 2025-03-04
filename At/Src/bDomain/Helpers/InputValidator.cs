using System.Globalization;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;

namespace CO2.At.Src.bDomain.Helpers;

public static class InputValidator
{

    public enum InputType
    {
        None = 0,                   //// チェックなし
        Numeric = 1,                //// 数字
        Alphabet = 2,               //// アルファベット
        Alphanumeric = 3,           //// アルファベット数字
        Symbolic = 4,               //// アルファベット数字記号
        FullWidth = 5,              //// 全角
        Date = 6,                   //// 日付
        HalfWidthKana = 7,          //// 半角カナ
        FullWidthKatakana = 8,      //// 全角カタカナ
    }

    ////正規表現を直接指定する場合は、IsMatch関数で全て代用可能
    public static bool IsRegMatch(string chkValue, string regPattern) => Regex.IsMatch(chkValue, regPattern);

    public static bool IsRegMatchWithRange(string chkValue, string regPattern, int minLength= -1, int maxLength = -1)
    {
        return Regex.IsMatch(chkValue, $"{regPattern}{{{minLength},{maxLength}}}$");               //文字列補完機能（$""）を使用して、minLengthとmaxLengthを変数として認識させる
    }

    ////========================================================================================
    ////データ型（全角、半角、数字等）、範囲、NULLの総合チェック
    //// 指定した型でかつ、指定した範囲であることのチェックをおこなう。
    //// 範囲を使わない場合は、最大長や0を指定することで省略可能
    ////========================================================================================

    public static bool IsValidate(string value, InputType type, bool isRequired)//入力必須ならisRequired：true、任意ならisRequired：false
    {

        string recvMsg;
        bool isValid;

        isValid = IsNullOrEmpty(value, out recvMsg);//nullまたは空文字の場合TRUEを返す

        //if (isRequired && isValid == false)//入力必須（NULL、空文字を許容しない）でNULLか空文字でない場合
        //{
        //    return false;
        //}
        //else if (isRequired == false && isValid == false)//入力任意（NULL、空文字を許容する）でNULLか空文字でない場合
        //{
        //    return true;
        //}
        if (isRequired && isValid == true)　//入力必須（NULL、空文字を許容しない）でNULLか空文字の場合
        {
            return false;
        }
        else if (isRequired == false && isValid == true)　//入力任意（NULL、空文字を許容する）でNULLか空文字の場合
        {
            return true;
        }

        //半角カナが含まれていたらFalseを返す式
        isValid = IsHalfWidthKatakana(value);

        if (isValid == false)
        {
            return false;
        }



        //ToDo:まず個別の単体テストが必要
        //例：IsValidNumberの場合：数字以外の、アルファベット、記号、全角、日付、半角カナが正しく異常になることをチェックする単体試験の実装

        switch (type)
        {
            case InputType.Numeric:
                isValid = IsValidNumber(value);
                break;
            case InputType.Alphabet:
                isValid = IsValidAlphabet(value);
                break;
            case InputType.Alphanumeric:
                isValid = IsValidAlphanumeric(value);
                break;
            case InputType.Symbolic:
                isValid = IsValidAlphanumericSymbols(value);
                break;
            case InputType.FullWidth:
                //ToDo:全角はほぼすべての型がOKなのでエラーがでる可能性が高いです。ここはしっかり単体テストをしてください。
                //isValid = IsValidFullWidth(value);

                isValid = true;
                break;
            case InputType.Date:
                isValid = IsValidDate(value);
                break;
            case InputType.HalfWidthKana:
                isValid = IsValidHalfWidthKatakana(value);
                break;
            case InputType.FullWidthKatakana:
                isValid = IsValidKatakana(value);
                break;
            default:
                isValid = false;
                break;
        }

        return isValid;

    }

    ////下記の要望がある場合は、汎用関数ではなく、下記の用途に応じた専用関数を利用してよい。
    ////1.引数の数を少なくしたい。（チェックする文字列だけ指定したい)
    ////2.関数名から全角ひらがなのみ、全角カタカナのみ、半角英数字のみなどの用途を明確にしたい
    ////3.正規表現を知らなくても、正規表現を利用してチェックしたい。

    public static bool IsValidNumber(string chkValue) => IsRegMatch(chkValue, "^[0-9]+$");                                  //半角数字のみが入力されているかチェック

    public static bool IsValidAlphabet(string chkValue) => IsRegMatch(chkValue, "^[a-zA-Z]+$");                              //半角英字のみが入力されているかチェック
    public static bool IsValidAlphanumeric(string chkValue) => IsRegMatch(chkValue, "^[a-zA-Z0-9]+$");                      //半角英数字のみが入力されているかチェック
    public static bool IsValidAlphanumericSymbols(string chkValue) => IsRegMatch(chkValue, "^[A-Za-z0-9!-/:-@[-´{-~]*$");  //半角英数字記号のみが入力されているかチェック
    public static bool IsValidHalfWidthKatakana(string chkValue) => IsRegMatch(chkValue, "^[ｱ-ﾝ]+$");                       //半角カタカナのみが入力されているかチェック
    public static bool IsValidKatakana(string chkValue) => IsRegMatch(chkValue, "^[ァ-ヴー]+$");                           //全角カタカナのみが入力されているかチェック

    //全角テストは行わない。理由:今回の全角の定義は、半角カナ以外のものを指す。半角カナの排除処理は別途実装するため。
    //public static bool IsValidFullWidth(string chkValue) => IsRegMatch(chkValue, "^[^\\x00-\\x7F]+$");                     //全角文字のみが入力されているかチェック


    //////未使用なのでコメントアウト。使用するときにコメントアウトを外し、単体テストを実装する。
    ////public static bool IsValidHiragana(string chkValue) => IsRegMatch(chkValue, "^[ぁ-んー]+$");        //全角ひらがなのみが入力されているかチェック
    ////public static bool IsValidHiraganaWithRange(string chkValue, int minLength, int maxLength) => IsRegMatchWithRange(chkValue, "^[ぁ-んー]+$", minLength, maxLength);       //全角ひらがなのみが入力されているかチェック
    ////public static bool IsValidKatakanaWithRange(string chkValue, int minLength, int maxLength) => IsRegMatchWithRange(chkValue, "^[ァ-ヴー]+$", minLength, maxLength);       //全角カタカナのみが入力されているかチェック
    ////public static bool IsValidHiraganaKatakanaWithRange(string chkValue, int minLength, int maxLength) => IsRegMatchWithRange(chkValue, "^[ぁ-んァ-ンー]+$", minLength, maxLength);  //全角ひらがな・カタカナのみが入力されているかチェック
    ////public static bool IsValidHiraganaAndKatakana(string chkValue) => IsRegMatch(chkValue, "^[ぁ-んァ-ンー]+$");  //全角ひらがなとカタカナのみが入力されているかチェック
    ////public static bool IsValidAlphanumericWithRange(string chkValue, int minLength, int maxLength) => IsRegMatchWithRange(chkValue, "^[a-zA-Z0-9]+$", minLength, maxLength);  //半角英数字のみが入力されているかチェック


    ////========================================================================================
    ////Length系個別チェック
    ////========================================================================================

    public static bool IsValidNumberRange(int targetNumber, int minNumber, int maxNumber)
    {
        return minNumber <= targetNumber && targetNumber <= maxNumber;
    }

    ////文字列長範囲チェック
    public static bool IsValidStrRange(string chkValue, int minLength, int maxLength)
    {
        return chkValue.Length >= minLength && chkValue.Length <= maxLength;
    }

    ////========================================================================================
    ////日付系個別チェック
    ////========================================================================================

    ////入力された日付が”yyyy/MM/dd”フォーマットに適合しているかどうかをチェック
    public static bool IsValidDate(string chkValue)
    {
        return DateTime.TryParseExact(chkValue, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    ////========================================================================================
    ////NULLチェック
    ////========================================================================================

    ////NULLチェック(エラーメッセージ不要のシンプルパターン)
    public static bool IsNullOrEmpty(string chkValue) => IsNullOrEmpty(chkValue, out _);

    ////NULLチェック(エラーメッセージありのパターン)
    public static bool IsNullOrEmpty(string chkValue, out string outputMsg)
    {
        if (string.IsNullOrEmpty(chkValue))//nullまたは空文字の場合TRUEを返す
        {
            outputMsg = "入力が必須です。";
            return true;
        }
        outputMsg = string.Empty;
        return false;
    }
    public static bool IsHalfWidthKatakana(string chkValue) => IsRegMatch(chkValue, "^[^ｱ-ﾝ]+$");                       //半角カタカナが一文字でも入っているとFalseを返す


    //////未使用なのでコメントアウト。使用するときにコメントアウトを外し、単体テストを実装する。
    //////リストの中に指定した項目が存在するかをチェック
    //public static bool IsExitItemInLists(string searchName, List<string> searchLists)
    //{
    //    return searchLists.Contains(searchName);
    //}
    //////（メールアドレスのチェックはしばらく使わないので保留）
    ////////メールアドレスが有効な形式かどうかを MailAddress コンストラクタを使ってチェック
    ////public static bool IsValidEmail(string email)
    ////{
    ////    string msgTmp;
    ////    try
    ////    {
    ////        var mailAddress = new MailAddress(email);
    ////        msgTmp = "メールアドレスは有効です。";
    ////        return true;
    ////    }
    ////    catch (FormatException)
    ////    {
    ////        msgTmp = "無効なメールアドレス形式です。\"";
    ////        return false;
    ////    }
    ////}
    ////////（パスワードの強度チェックはしばらく使わないので保留）
    //////パスワードの強度チェック
    //////少なくとも一つの数字、一つの小文字、一つの大文字を含み、かつ8文字以上であることを要求
    //public static bool IsValidPassword(string password)
    //{
    //    return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$");
    //}






}
