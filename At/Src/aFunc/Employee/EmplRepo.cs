using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Customer.Parts;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;
using Windows.Devices.Usb;
using Xunit;

namespace CO2.At.Src.aFunc.Employee;

public class EmplRepo : IEmp
{
    private readonly DBAbstract _dbAbstract;
    private readonly SqlConnection _sqlCon;

    public EmplRepo(DBAbstract dbAbstract)
    {
        _dbAbstract = dbAbstract;
        _sqlCon = _dbAbstract.Get_SqlConnection() ?? throw new CustomNullException($"データベース初期設定で致命的エラー発生:要因:connectionのNULLエラー {nameof(_sqlCon)}");
    }

    public List<EParent> LoadP()
    {
        List<EParent> records = new ();
        string sqlQuery = "SELECT * FROM Organization_Parent WHERE delete_flag = 0";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new (sqlQuery, _sqlCon);

            using SqlDataReader reader = cmd.ExecuteReader();
            ////SqlDataReaderはエラー時にNULLではなく、例外をスローするのでNULLチェックは不要。

            while (reader.Read())
            {
                records.Add(CreateEParent(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"Load:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;
    }

    public List<EChild> LoadC()
    {
        List<EChild> records = new List<EChild>();
        string sqlQuery = "SELECT * FROM Organization_Child WHERE delete_flag = 0";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new (sqlQuery, _sqlCon);

            using SqlDataReader reader = cmd.ExecuteReader();
            ////SqlDataReaderはエラー時にNULLではなく、例外をスローするのでNULLチェックは不要。

            while (reader.Read())
            {
                records.Add(CreateEChild(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"Load:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;
    }

    public List<EEmp> LoadE()
    {
        List<EEmp> records = new List<EEmp>();
        string sqlQuery = "SELECT * FROM employee WHERE delete_flag = 0";

        try
        {
            _dbAbstract.ConnectionStateOpenAndNullGuard();

            using SqlCommand cmd = new (sqlQuery, _sqlCon);

            using SqlDataReader reader = cmd.ExecuteReader();
            ////SqlDataReaderはエラー時にNULLではなく、例外をスローするのでNULLチェックは不要。

            while (reader.Read())
            {
                records.Add(CreateEEmp(reader));
            }
        }
        catch (Exception ex)
        {
            records.Clear();
            DBAbstract.DBSqlExcept(ex, $"Load:読み出しエラー:SQL: {sqlQuery} \nStackTrace: {ex.StackTrace}");
        }
        return records;
    }



    public bool CreateP(EParent eOrganizationParent)
    {
        string sqlQuery = @"INSERT INTO Organization_Parent (organization_parent_name, delete_flag, effective_end_date,
                                                                    created_at, updated_at, updated_user, is_old_existence)
                                VALUES (@OrgParentName, 0, 0, DEFAULT, GETDATE(), 1, 0)";

        try
        {
            using SqlCommand cmd = new (sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@OrgParentName", eOrganizationParent.OrgParentName);

            int affectedRows = cmd.ExecuteNonQuery();
            HLog.Info($"{affectedRows} rows inserted into the database");

            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            HLog.Info($"SQL Insert Error: {ex.Message} \nQuery: {sqlQuery}");
            return false;
        }
    }

    public EParent? ReadP(int targetID)
    {
        string sqlQuery = "SELECT * FROM Organization_Parent WHERE organization_parent_id = @OrgParentName AND delete_flag = 0";

        try
        {
            using SqlCommand cmd = new(sqlQuery, _sqlCon);
            cmd.Parameters.AddWithValue("@OrgParentName", targetID);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                EParent eOrganizationParent = new();
                CreateEParent(reader);
                Debug.WriteLine(eOrganizationParent?.DeleteFlag);
                return eOrganizationParent;
            }
        }
        catch (Exception ex)
        {
            HLog.Info($"SQL Read Error: {ex.Message} \nQuery: {sqlQuery}");
        }
        return null;
    }

    public bool UpdateP(EParent eOrganizationParent)
    {
        string sqlQuery = @"
                UPDATE Organization_Parent 
                SET organization_parent_name = @OrgParentName,
                    updated_at = GETDATE()
                WHERE organization_parent_id = @OrgParentId";

        try
        {
            using (SqlCommand cmd = new (sqlQuery, _sqlCon))
            {
                cmd.Parameters.AddWithValue("@OrgParentName", eOrganizationParent.OrgParentName);
                cmd.Parameters.AddWithValue("@OrgParentId", eOrganizationParent.OrgParentId);

                int affectedRows = cmd.ExecuteNonQuery();
                HLog.Info($"{affectedRows} rows updated");
                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            HLog.Info($"SQL Update Error: {ex.Message} \nQuery: {sqlQuery} \nParameter: {eOrganizationParent.OrgParentName}");
            return false;
        }
    }

    public bool DeleteP(int targetID)
    {

        string sqlQuery = "UPDATE Organization_Parent SET delete_flag=1 WHERE organization_parent_id = @OrgParentId";

        try
        {
            using (SqlCommand cmd = new (sqlQuery, _sqlCon))
            {
                cmd.Parameters.AddWithValue("@OrgParentId", targetID);

                int affectedRows = cmd.ExecuteNonQuery();
                HLog.Info($"{affectedRows} rows updated");

                return affectedRows > 0;
            }
        }
        catch (Exception ex)
        {
            HLog.Err($"Organization Parent Delete Error: {ex.Message} \nQuery: {sqlQuery} \nParameter] {targetID}");
            return false;
        }
    }

    private EParent CreateEParent(SqlDataReader reader)
    {
        EParent eParent = new ();

        try
        {
            eParent.Set(
                orgParentId: SqlDataHelper.GetColVal<int>(reader, "organization_parent_id"),
                orgParentName: SqlDataHelper.GetColVal<string>(reader, "organization_parent_name"),
                deleteFlag: SqlDataHelper.GetColVal<bool>(reader, "delete_flag"));
        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
        return eParent;
    }

    private EChild CreateEChild(SqlDataReader reader)
    {
        EChild eChild = new ();

        try
        {
            eChild.Set(
                orgChildId: SqlDataHelper.GetColVal<int>(reader, "organization_child_id"),
                orgChildName: SqlDataHelper.GetColVal<string>(reader, "organization_child_name"),
                orgParentId: SqlDataHelper.GetColVal<int>(reader, "organization_parent_id"));
        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
        return eChild;
    }

    private EEmp CreateEEmp(SqlDataReader reader)
    {
        EEmp eEmp = new();

        try
        {
            eEmp.Set(
                empId: SqlDataHelper.GetColVal<int>(reader, "employee_id"),
                empName: SqlDataHelper.GetColVal<string>(reader, "employee_name"),
                orgChildId: SqlDataHelper.GetColVal<int>(reader, "organization_child_id"));
        }
        catch (Exception ex)
        {
            HLog.Err($"データベースのフィールドをEntityフィールドに格納するときにエラー発生。:トレース情報: {ex.Message} \nStack Trace: {ex.StackTrace}");
            throw;
        }
        return eEmp;
    }

    public int GetMaxOrganizationId()
    {
        string sqlQuery = "SELECT TOP 1 * FROM Organization_Parent ORDER BY organization_parent_id DESC";

        using (SqlCommand cmd = new(sqlQuery, _sqlCon))
        {
            try
            {
                object? result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception ex)
            {
                HLog.Err($"Error in GetMaxOrganizationId: {ex.Message} \n{ex.StackTrace}");
                return -1;
            }
        }
    }


    public bool ChkLogin(string userName, string password)
    {
        string query = "SELECT * FROM [User] WHERE user_name = @userName";

        using (SqlCommand cmd = new (query, _sqlCon))
        {
            cmd.Parameters.AddWithValue($"@userName", userName);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return password == reader["password"].ToString();
                }
            }
        }
        return false;
    }

}
