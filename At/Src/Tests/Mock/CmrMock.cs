using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.bDomain.Helpers;

#if UNIT_TEST

namespace CO2.At.Src.Tests.Mock;

public sealed class CmrMock : ICustomer
{
    private static readonly Random _random = new ();
    private List<ECustomer> _cList = null;

    public CmrMock()
    {
        ////View定型処理
        Load();
    }

    public List<ECustomer> Load()
    {
        string[] firstFurigana = { "アンドウ", "スズキ", "タカハシ", "タナカ", "ワタナベ", "イトウ", "ヤマモト", "ナカムラ", "コバヤシ", "カトウ" };
        string[] lastFurigana = { "イチロウ", "ジロウ", "サブロウ", "シロウ", "ゴロウ", "ロクロウ", "シチロウ", "ハチロウ", "キュウロウ", "ジュウロウ" };
        string[] firstNames = { "安藤", "次郎", "三郎", "四郎", "五郎", "六郎", "七郎", "八郎", "九郎", "十郎" };
        string[] lastNames = { "佐藤", "鈴木", "高橋", "田中", "渡辺", "伊藤", "山本", "中村", "小林", "加藤" };

        _cList = new ();

        for (int i = 0; i < 50; i++)
        {
            ECustomer customerHead = new();
            //ToDo:引数が追加されたので要修正 _を指定してもよい。
            string furigana = firstFurigana[i % firstFurigana.Length] + " " + lastFurigana[i % lastFurigana.Length];
            string name = lastNames[i % lastNames.Length] + " " + firstNames[i % firstNames.Length];

            customerHead.Set(
                            i + 1,
                            furigana,
                            name,
                            1,
                            Convert.ToDateTime("1980/01/11"),
                            1,
                            1,
                            1,
                            i.ToString() + "会社",
                            "070-4801-8810",
                            "666-1001",
                            "大阪府大阪市1",
                            "大阪府大阪市2",
                            "070-4801-8810",
                            "06-6208-1001",
                            "06-6208-1001",
                            "123-1001",
                            "海山県湖丘市1",
                            "海山県湖丘市2",
                            1,
                            "1234567890123",
                            "宇宙銀行",
                            "天の川支店",
                            1,
                            "1234567",
                            false,
                            Convert.ToDateTime("2024-01-01"),
                            Convert.ToDateTime("2024/01/01"),
                            Convert.ToDateTime("2024/01/01"),
                            Convert.ToDateTime("2099/01/01"));
            _cList.Add(customerHead);

        }
        return _cList;
    }
    public List<ECustomer> SearchByVowel(string vowelKindName)
    {
        var result = _cList.Where(customer =>
       customer.Furigana[0] == vowelKindName[0])
       .ToList();
        return new List<ECustomer>(result);
        //{
        //    new ECustomer { FullName = initial, Furigana = "クリス" },
        //    new ECustomer { FullName = "アン" , Furigana = "アン" },
        //};
    }


    public List<ECustomer>? PartialSearch(string fullName, string furigana, string personalPhone, string companyPhone) => null;

    public bool Create(ECustomer ecustomer) => true;

    public ECustomer Read(int target_number)
    {
        ECustomer ecustomer = new ();

        ecustomer.AutoId = GenerateRandomNumber(10001, 90000);
        ecustomer.CustomerNumber = ecustomer.AutoId;
        ecustomer.Furigana = "ヤマダ　タロウ"; // 例: フリガナ
        ecustomer.FullName = "山田 太郎"; // 例: フルネーム
        ecustomer.Gender.SetGender(1); // 例: 性別（1=男性）
        ecustomer.Birthday.SetBirthday(Convert.ToDateTime("1973-10-14"));

        ecustomer.MailingDestinationType = 0; // 例: 宅配先タイプ
        ecustomer.ContactKind = 1; // 例: 連絡種別
        ecustomer.RepresentativeId = 100; // 例: 代表者ID

        ecustomer.EmpName = "株式会社サンプル"; // 例: 会社名
        ecustomer.EmpPhoneNumber.SetPhoneNumber("03-1234-5678"); // 例: 会社電話番号
        ecustomer.EmpAddress.SetPostalCode("123-4567"); // 例: 会社郵便番号
                                                                 //        ecustomer.EmployerAddress = "東京都新宿区"; // 例: 会社住所
        ecustomer.EmpAddress.SetAddress("1243");
        ecustomer.EmpAddress2 = "ビル名"; // 例: 会社住所2

        ecustomer.PsnPhoneNumber.SetPhoneNumber("080-1234-5678"); // 例: 個人電話番号
        ecustomer.PsnFaxNumber.SetPhoneNumber("03-9876-5432"); // 例: 個人FAX番号
        ecustomer.PsnMobileNumber.SetPhoneNumber("090-1234-5678"); // 例: 個人携帯番号
        ecustomer.PsnAddress.SetPostalCode("987-6543"); // 例: 個人郵便番号
        ecustomer.PsnAddress.SetAddress("東京都渋谷区"); // 例: 個人住所
        ecustomer.PsnAddress2 = "アパート名"; // 例: 個人住所2

        ecustomer.SubmissionCertificate.SetDocumentKind(1); // 例: 届出事項の種類
        ecustomer.SubmissionCertificate.SetCertificationNumber("123456"); // 例: 証明書番号
        ecustomer.PsnBankAccounts.SetBankName("サンプル銀行"); // 例: 銀行名
        ecustomer.PsnBankAccounts.SetBranchName("本店"); // 例: 支店名
        ecustomer.PsnBankAccounts.SetAccountKind(2); // 例: 口座種別
        ecustomer.PsnBankAccounts.SetAccountNumber("123456789");  // 例: 口座番号

        ecustomer.DeleteFlag = false; // 例: 削除フラグ
        ecustomer.AutoUpdatedAt = DateTime.Now; // 更新日時
        ecustomer.AutoCreatedAt = DateTime.Now; // 作成日時
        ecustomer.AutoEffectiveStartDate = DateTime.Now; // 有効開始日
        ecustomer.AutoEffectiveEndDate = DateTime.Now.AddYears(1); // 有効終了日

        return ecustomer;
    }

    public bool Update(ECustomer eCustomer) => true;

    public bool Delete(int targetID) => true;


    public int GetNewID() => GenerateRandomNumber(10001, 90000);

    private int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);

    public List<ECustomer> LoadDeleteCustomers()
    {
        throw new NotImplementedException();
    }
}
#endif
