using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CO2.At.Src.bInfrastructure.DB;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace CO2.At.Src.aFunc.GoodCurrency;

public sealed class GoodRepo : IGood
{
    private readonly DBAbstract _dbAbstract;
    private readonly SqlConnection _sqlCon;
    private int _crudMode;

    public GoodRepo(DBAbstract dbAbstract)
    {
        _dbAbstract = dbAbstract;
        _sqlCon = _dbAbstract.Get_SqlConnection() ?? throw new CustomNullException($"データベース初期設定で致命的エラー発生:要因:connectionのNULLエラー {nameof(_sqlCon)}");
    }

    public ECurrency ReadC(int targetID)
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
                return CreateECurrency(reader);
            }
        }
        catch (Exception ex)
        {
            DBAbstract.DBSqlExcept(ex, $"顧客:Read:読み出しエラー:SQL: {sqlQuery}");
        }
        return null;
    }

    public List<ECurrency> LoadC()
    {
        List<ECurrency> records = new ();

        string sqlQuery = @"SELECT 
                                cb.base_rate,
                                cb.updated_at,
                                c.base_currency_id,
                                c.name,
                                c.unit
                            FROM Currency c
                            INNER JOIN Currency_BaseRate cb
                                ON c.base_currency_id = cb.base_currency_id;
                            ";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new(sqlQuery, _sqlCon);
            using SqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("@DeleteFlag", 0);

            ////SqlDataReaderはエラー時にNULLではなく、例外をスローするのでNULLチェックは不要。
            while (reader.Read())
            {
                records.Add(CreateECurrency(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"LoadC:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;

    }

    public List<EGood> LoadG()
    {
        List<EGood> records = new();

        string sqlQuery = @"SELECT 
                                gb.updated_at,
                                gb.standard_price,
                                g.product_id,
                                g.product_name,
                                g.base_currency_id,
                                g.transaction_unit,
                                g.transaction_margin,
                                g.basic_fee,
                                g.minimum_quantity
                            FROM Good g
                            INNER JOIN Good_BaseRate gb
                                ON g.product_id = gb.product_id
                            WHERE g.delete_flag = @DeleteFlag;
                            ";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new(sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@DeleteFlag", 0);

            using SqlDataReader reader = cmd.ExecuteReader();

            ////SqlDataReaderはエラー時にNULLではなく、例外をスローするのでNULLチェックは不要。
            while (reader.Read())
            {
                records.Add(CreateEGood(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"LoadG:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;

    }

    public bool CreateC(ECurrency eCurrency)
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

    public bool UpdateC(ECurrency eCurrency)
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

                int affectedRows = cmd.ExecuteNonQuery();
                HLog.Info($"{affectedRows} rows updated.");
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DeleteC(int targetID)
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





    private static ECurrency CreateECurrency(SqlDataReader reader)
    {
        try
        {
            var entity = new ECurrency();
            entity.Set(
                baseRate: SqlDataHelper.GetColVal<decimal>(reader, "base_rate"),
                updatedAt: SqlDataHelper.GetColVal<DateTime>(reader, "updated_at"),
                baseCurrencyId: SqlDataHelper.GetColVal<int>(reader, "base_currency_id"),
                name: SqlDataHelper.GetColVal<string>(reader, "name"),
                unit: SqlDataHelper.GetColVal<string>(reader, "unit"));
            return entity;
        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
    }

    private static EGood CreateEGood(SqlDataReader reader)
    {
        try
        {
            var entity = new EGood();
            entity.Set(
                standardPrice: SqlDataHelper.GetColVal<decimal>(reader, "standard_price"),
                updatedAt: SqlDataHelper.GetColVal<DateTime>(reader, "updated_at"),
                productId: SqlDataHelper.GetColVal<int>(reader, "product_id"),
                productName: SqlDataHelper.GetColVal<string>(reader, "product_name"),
                baseCurrencyId: SqlDataHelper.GetColVal<int>(reader, "base_currency_id"),
                transactionUnit: SqlDataHelper.GetColVal<string>(reader, "transaction_unit"),
                transactionMargin: SqlDataHelper.GetColVal<decimal>(reader, "transaction_margin"),
                basicFee: SqlDataHelper.GetColVal<decimal>(reader, "basic_fee"),
                minimumQuantity: SqlDataHelper.GetColVal<int>(reader, "minimum_quantity"));
            return entity;

        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
    }



}
