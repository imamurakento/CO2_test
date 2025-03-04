using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CO2.At.Src.bInfrastructure.DB;
using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace CO2.At.Src.aFunc.Customer;

public sealed class CustomerRepository : ICustomer
{
    private readonly DBAbstract _dbAbstract;
    private readonly SqlConnection _sqlCon;

    public CustomerRepository(DBAbstract dbAbstract)
    {
        _dbAbstract = dbAbstract;
        _sqlCon = _dbAbstract.Get_SqlConnection() ?? throw new CustomNullException($"データベース初期設定で致命的エラー発生:要因:connectionのNULLエラー {nameof(_sqlCon)}");
    }

    ////    graph TD;
    ////    A[Read()] -->|SQL実行| B(SqlCommand)
    ////    B -->|ExecuteReader| C(SqlDataReader)
    ////    C -->|データ取得| D[CreateECustomer()]
    ////    D -->|フィールド変換| E[GetColVal<T>()]
    ////    E -->|型変換| F[Convert.ChangeType()]
    ////    E -->|エラー| G{Exception
    ////}
    ////    G -->|DBエラー| H[DBSqlExcept()]
    ////    G -->|型変換エラー| I[CustomDbSQLException]
    ////    H -->|ログ記録| J[HLog.Err()]
    public ECustomer? Read(int targetID)
    {
        string sqlQuery = "SELECT * FROM Customer WHERE customer_number = @CustomerNumber";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new (sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@CustomerNumber", targetID);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return CreateECustomer(reader);
            }
        }
        catch (Exception ex)
        {
            DBAbstract.DBSqlExcept(ex, $"顧客:Read:読み出しエラー:SQL: {sqlQuery}");
        }
        return null;
    }

    public List<ECustomer> Load()
    {
        List<ECustomer> records = new ();
        string sqlQuery = "SELECT * FROM Customer WHERE delete_flag = @DeleteFlag";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new(sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@DeleteFlag", 0);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                records.Add(CreateECustomer(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"顧客:Load:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;

    }

    public List<ECustomer> LoadDeleteCustomers()
    {
        List<ECustomer> records = new();
        string sqlQuery = "SELECT * FROM Customer WHERE delete_flag = @DeleteFlag";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new(sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@DeleteFlag", 1);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                records.Add(CreateECustomer(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"顧客:Load:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;

    }



    public bool Create(ECustomer eCustomer)
    {
        string sqlQuery = @"
                    INSERT INTO Customer (customer_number,furigana, full_name, gender, birth_date,mailing_destination_type, contact_kind,
                                            representative_id, employer_name,employer_phone_number, employer_postal_code, employer_address, employer_address2,personal_phone_number, 
                                            personal_fax_number, personal_mobile_number,personal_postal_code, personal_address, personal_address2,
                                            notifymatters_documents_kind, notifymatters_certification_number, 
                                            notifymatters_bank_name, notifymatters_branch_name, notifymatters_account_kind, 
                                            notifymatters_account_number ) 
                    VALUES (@CustomerNumber,
                            @Furigana,
                            @FullName,
                            @Gender,
                            @BirthDate,
                            @MailingDestinationType,
                            @ContactKind,
                            @RepresentativeId,
                            @EmployerName,
                            @EmployerPhoneNumber,
                            @EmployerPostalCode,
                            @EmployerAddress,
                            @EmployerAddress2,
                            @PersonalPhoneNumber,
                            @PersonalFaxNumber,
                            @PersonalMobileNumber, 
                            @PersonalPostalCode,
                            @PersonalAddress,
                            @PersonalAddress2, 
                            @NotifyMattersDocumentsKind,
                            @NotifyMattersCertificationNumber, 
                            @NotifyMattersBankName,
                            @NotifyMattersBranchName,
                            @NotifyMattersAccountKind,
                            @NotifyMattersAccountNumber
                    )";

        try
        {

            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using (SqlCommand cmd = new (sqlQuery, _sqlCon))
            {
                cmd.Parameters.AddWithValue("@CustomerNumber", eCustomer.CustomerNumber);
                cmd.Parameters.AddWithValue("@Furigana", eCustomer.Furigana);
                cmd.Parameters.AddWithValue("@FullName", eCustomer.FullName);
                cmd.Parameters.AddWithValue("@Gender", eCustomer.Gender.GetGender());
                cmd.Parameters.AddWithValue("@BirthDate", eCustomer.Birthday.GetBirthday());
                cmd.Parameters.AddWithValue("@MailingDestinationType", eCustomer.MailingDestinationType);
                cmd.Parameters.AddWithValue("@ContactKind", eCustomer.ContactKind);

                cmd.Parameters.AddWithValue("@RepresentativeId", eCustomer.RepresentativeId);
                cmd.Parameters.AddWithValue("@EmployerName", eCustomer.EmpName);
                cmd.Parameters.AddWithValue("@EmployerPhoneNumber", eCustomer.EmpPhoneNumber.GetPhoneNumber());
                cmd.Parameters.AddWithValue("@EmployerPostalCode", eCustomer.EmpAddress.GetPostalCode());
                cmd.Parameters.AddWithValue("@EmployerAddress", eCustomer.EmpAddress.GetAddress());
                cmd.Parameters.AddWithValue("@EmployerAddress2", eCustomer.EmpAddress2);

                cmd.Parameters.AddWithValue("@PersonalPhoneNumber", eCustomer.PsnPhoneNumber.GetPhoneNumber());
                cmd.Parameters.AddWithValue("@PersonalFaxNumber", eCustomer.PsnFaxNumber.GetPhoneNumber());
                cmd.Parameters.AddWithValue("@PersonalMobileNumber", eCustomer.PsnMobileNumber.GetPhoneNumber());
                cmd.Parameters.AddWithValue("@PersonalPostalCode", eCustomer.PsnAddress.GetPostalCode());
                cmd.Parameters.AddWithValue("@PersonalAddress", eCustomer.PsnAddress.GetAddress());
                cmd.Parameters.AddWithValue("@PersonalAddress2", eCustomer.PsnAddress2);

                cmd.Parameters.AddWithValue("@NotifyMattersDocumentsKind", eCustomer.SubmissionCertificate.GetDocumentKind());
                cmd.Parameters.AddWithValue("@NotifyMattersCertificationNumber", eCustomer.SubmissionCertificate.GetCertificationNumber());
                cmd.Parameters.AddWithValue("@NotifyMattersBankName", eCustomer.PsnBankAccounts.GetBankName());
                cmd.Parameters.AddWithValue("@NotifyMattersBranchName", eCustomer.PsnBankAccounts.GetBranchName());
                cmd.Parameters.AddWithValue("@NotifyMattersAccountKind", eCustomer.PsnBankAccounts.GetAccountKind());
                cmd.Parameters.AddWithValue("@NotifyMattersAccountNumber", eCustomer.PsnBankAccounts.GetAccountNumber());

                //// Execute the insert command and get the number of affected rows
                int affectedRows = cmd.ExecuteNonQuery();
                HLog.Info($"{affectedRows} rows inserted into the database.");

                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            DBAbstract.DBSqlExcept(ex, $"顧客:Create:エラー:SQL: {sqlQuery}");
            return false;
        }
    }

    //ToDo:パラメータが明らかに少ないので、リファクタリングする

    public bool Update(ECustomer eCustomer)
    {
        string sqlQuery = @"
                    UPDATE Customer SET
                        furigana = @Furigana,
                        full_name = @FullName,
                        gender = @Gender,
                        birth_date = @BirthDate,
                        mailing_destination_type = @MailingDestinationType,
                        representative_id = @RepresentativeId,
                        employer_name = @EmployerName,
                        employer_phone_number = @EmployerPhoneNumber,
                        employer_postal_code = @EmployerPostalCode,
                        employer_address = @EmployerAddress,
                        employer_address2 = @EmployerAddress2
                    WHERE customer_number = @CustomerNumber;";

        try
        {

            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using (SqlCommand cmd = new (sqlQuery, _sqlCon))
            {
                //// Add parameters to command to avoid SQL injection
                cmd.Parameters.AddWithValue("@CustomerNumber", eCustomer.CustomerNumber);

                cmd.Parameters.AddWithValue("@Furigana", eCustomer.Furigana);
                cmd.Parameters.AddWithValue("@FullName", eCustomer.FullName);
                cmd.Parameters.AddWithValue("@Gender", eCustomer.Gender.GetGender());
                cmd.Parameters.AddWithValue("@BirthDate", eCustomer.Birthday.GetBirthday());
                cmd.Parameters.AddWithValue("@MailingDestinationType", eCustomer.MailingDestinationType);
                cmd.Parameters.AddWithValue("@RepresentativeId", eCustomer.RepresentativeId);

                cmd.Parameters.AddWithValue("@EmployerName", eCustomer.EmpName);
                cmd.Parameters.AddWithValue("@EmployerPhoneNumber", eCustomer.EmpPhoneNumber.GetPhoneNumber());
                cmd.Parameters.AddWithValue("@EmployerPostalCode", eCustomer.EmpAddress.GetPostalCode());
                cmd.Parameters.AddWithValue("@EmployerAddress", eCustomer.EmpAddress.GetAddress());
                cmd.Parameters.AddWithValue("@EmployerAddress2", eCustomer.EmpAddress2);

                int affectedRows = cmd.ExecuteNonQuery();
                HLog.Info($"{affectedRows} rows updated.");
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            DBAbstract.DBSqlExcept(ex, $"顧客:Update:エラー:SQL: {sqlQuery} CustomerNumber: {eCustomer.CustomerNumber}");
            return false;
        }
    }

    public bool Delete(int targetID)
    {
        string sqlQuery = "UPDATE Customer SET delete_flag=1 WHERE customer_number = @target_number";

        try
        {

            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new (sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@target_number", targetID);

            int affectedRows = cmd.ExecuteNonQuery();

            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            DBAbstract.DBSqlExcept(ex, $"顧客:Delete:エラー:SQL: {sqlQuery} CustomerNumber: {targetID}");
            return false;
        }
    }

    public int GetNewID()
    {
        // SQLクエリ: 最大値を取得する
        string query = "SELECT MAX(customer_number) FROM Customer";
        using (SqlCommand cmd = new (query, _sqlCon))
        {
            object? result = cmd.ExecuteScalar();

            int maxCustomerNumber = 0;

            //ToDo:取れなかったら不具合なので、例外に変更する
            if (result != DBNull.Value && result != null)
            {
                maxCustomerNumber = Convert.ToInt32(result);
                Debug.WriteLine($"最大の cunstomer_number: {maxCustomerNumber}");
            }
            else
            {
                Debug.WriteLine("customer_number の最大値を取得できませんでした。");
            }

            //最大値+1の値を返却
            return maxCustomerNumber + 1;
        }
    }


    public List<ECustomer>? SearchByVowel(string vowelKindName)
    {
        List<ECustomer> cList = new ();

        // 五十音行ごとの対応表
        Dictionary<string, List<string>> kanaGroups = new ()
        {
            { "ア", new List<string> { "ア", "イ", "ウ", "エ", "オ" } },
            { "カ", new List<string> { "カ", "ガ", "キ", "ギ", "ク", "グ", "ケ", "ゲ", "コ", "ゴ" } },
            { "サ", new List<string> { "サ", "ザ", "シ", "ジ", "ス", "ズ", "セ", "ゼ", "ソ", "ゾ" } },
            { "タ", new List<string> { "タ", "ダ", "チ", "ヂ", "ツ", "ヅ", "テ", "デ", "ト", "ド" } },
            { "ナ", new List<string> { "ナ", "ニ", "ヌ", "ネ", "ノ" } },
            { "ハ", new List<string> { "ハ", "バ", "パ", "ヒ", "ビ", "ピ", "フ", "ブ", "プ", "ヘ", "ベ", "ペ", "ホ", "ボ", "ポ" } },
            { "マ", new List<string> { "マ", "ミ", "ム", "メ", "モ" } },
            { "ヤ", new List<string> { "ヤ", "ユ", "ヨ" } },
            { "ラ", new List<string> { "ラ", "リ", "ル", "レ", "ロ" } },
            { "ワ", new List<string> { "ワ", "ヲ", "ン" } },
        };

        ////不要：該当なしはバグ以外のケースではない。
        //// 入力された文字がどの行に該当するか確認
        //if (!kanaGroups.ContainsKey(vowelKindName))
        //{
        //    return null; // 該当なしの場合は何もしない
        //}

        try
        {

            List<string> targetGroup = kanaGroups[vowelKindName];

            // SQLクエリを動的に作成（指定された行のカナをすべて検索対象にする）
            string query = "SELECT * FROM Customer WHERE delete_flag = 0 AND (" +
                           string.Join(" OR ", targetGroup.Select((k, i) => $"furigana LIKE @Keyword{i} + '%'")) + ")";

            using SqlCommand cmd = new (query, _sqlCon);
            for (int i = 0; i < targetGroup.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@Keyword{i}", targetGroup[i]);
            }

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cList.Add(CreateECustomer(reader));
            }
        }
        catch (Exception ex)
        {
            cList.Clear();
            HLog.Except(ex);
        }
        return cList;
    }

    public List<ECustomer>? PartialSearch(string fullName, string furigana, string personalPhone, string companyPhone)
    {
        // nullチェックのアサート
        //Debug.Assert(!string.IsNullOrEmpty(key), "The 'key' parameter must not be null or empty.");
        Debug.Assert(fullName != null, "The 'fullName' parameter must not be null.");
        Debug.Assert(furigana != null, "The 'furigana' parameter must not be null.");
        Debug.Assert(personalPhone != null, "The 'personalPhone' parameter must not be null.");
        Debug.Assert(companyPhone != null, "The 'companyPhone' parameter must not be null.");

        // 検索結果を格納するリスト
        List<ECustomer> cList = new ();

        SqlBuilder hMakeSql = new ();
        //string query = hMakeSql.BuildPartialMatchQuery(fullName, furigana, personalPhone, companyPhone);

        string query = $@"
            SELECT * FROM Customer 
            WHERE delete_flag = 0 
            AND full_name LIKE @fullName
            AND furigana LIKE @furigana
            AND personal_phone_number LIKE @personalPhone
            AND employer_phone_number LIKE @companyPhone";

        using (SqlCommand cmd = new (query, _sqlCon))
        {
            // パラメータ化クエリで値を設定
            cmd.Parameters.AddWithValue("@fullName", string.IsNullOrWhiteSpace(fullName) ? "%" : $"%{fullName}%");
            cmd.Parameters.AddWithValue("@furigana", string.IsNullOrWhiteSpace(furigana) ? "%" : $"%{furigana}%");
            cmd.Parameters.AddWithValue("@personalPhone", string.IsNullOrWhiteSpace(personalPhone) ? "%" : $"%{personalPhone}%");
            cmd.Parameters.AddWithValue("@companyPhone", string.IsNullOrWhiteSpace(companyPhone) ? "%" : $"%{companyPhone}%");

            try
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cList.Add(CreateECustomer(reader));
                }
            }
            catch (Exception ex)
            {
                cList.Clear();
                HLog.Except(ex);
            }
        }

        return cList;
    }

    private static ECustomer CreateECustomer(SqlDataReader reader)
    {
        ECustomer eCustomer = new ();

        try
        {
            eCustomer.Set(
                customerNumber: SqlDataHelper.GetColVal<int>(reader, "customer_number"),
                furigana: SqlDataHelper.GetColVal<string>(reader, "furigana"),
                fullName: SqlDataHelper.GetColVal<string>(reader, "full_name"),
                gender: SqlDataHelper.GetColVal<byte>(reader, "gender"),
                birthDate: SqlDataHelper.GetColVal<DateTime>(reader, "birth_date"),
                mailingDestinationType: SqlDataHelper.GetColVal<byte>(reader, "mailing_destination_type"),
                contactKind: SqlDataHelper.GetColVal<byte>(reader, "contact_kind"),
                representativeId: SqlDataHelper.GetColVal<int>(reader, "representative_id"),
                employerName: SqlDataHelper.GetColVal<string>(reader, "employer_name"),
                employerPhoneNumber: SqlDataHelper.GetColVal<string>(reader, "employer_phone_number"),
                employerPostalCode: SqlDataHelper.GetColVal<string>(reader, "employer_postal_code"),
                employerAddress: SqlDataHelper.GetColVal<string>(reader, "employer_address"),
                employerAddress2: SqlDataHelper.GetColVal<string>(reader, "employer_address2"),
                personalPhoneNumber: SqlDataHelper.GetColVal<string>(reader, "personal_phone_number"),
                personalFaxNumber: SqlDataHelper.GetColVal<string>(reader, "personal_fax_number"),
                personalMobileNumber: SqlDataHelper.GetColVal<string>(reader, "personal_mobile_number"),
                personalPostalCode: SqlDataHelper.GetColVal<string>(reader, "personal_postal_code"),
                personalAddress: SqlDataHelper.GetColVal<string>(reader, "personal_address"),
                personalAddress2: SqlDataHelper.GetColVal<string>(reader, "personal_address2"),
                notifymattersDocumentsKind: SqlDataHelper.GetColVal<byte>(reader, "notifymatters_documents_kind"),
                notifymattersCertificationNumber: SqlDataHelper.GetColVal<string>(reader, "notifymatters_certification_number"),
                notifymattersBankName: SqlDataHelper.GetColVal<string>(reader, "notifymatters_bank_name"),
                notifymattersBranchName: SqlDataHelper.GetColVal<string>(reader, "notifymatters_branch_name"),
                notifymattersAccountKind: SqlDataHelper.GetColVal<byte>(reader, "notifymatters_account_kind"),
                notifymattersAccountNumber: SqlDataHelper.GetColVal<string>(reader, "notifymatters_account_number"),
                deleteFlag: SqlDataHelper.GetColVal<bool>(reader, "delete_flag"),
                autoUpdatedAt: SqlDataHelper.GetColVal<DateTime>(reader, "auto_updated_at"),
                autoCreatedAt: SqlDataHelper.GetColVal<DateTime>(reader, "auto_created_at"),
                autoEffectiveStartDate: SqlDataHelper.GetColVal<DateTime>(reader, "auto_effective_start_date"),
                autoEffectiveEndDate: SqlDataHelper.GetColVal<DateTime>(reader, "auto_effective_end_date"));
        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
        return eCustomer;
    }





}
