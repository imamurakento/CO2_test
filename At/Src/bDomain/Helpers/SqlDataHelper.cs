using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.bDomain.Entities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace CO2.At.Src.bDomain.Helpers;

public static class SqlDataHelper
{

    public static T GetColVal<T>(SqlDataReader reader, string columnName)
    {
        try
        {
            int ordinal = reader.GetOrdinal(columnName);
            string columType = reader.GetDataTypeName(ordinal);

            string logReportMsg = $"フィールド名: {columnName} DB型: {columType} Entity型: {typeof(T).Name}";
#if DB_MIGRATION
            HLog.Dbg(logReportMsg);
#endif
            ////ToDo:データ移行モードのときは、ここに異常データを検出するためのロジックを追加する。
            if (reader.IsDBNull(ordinal))
            {
#if DB_MIGRATION
                HLog.Dbg($"データ移行モード: DBからの取得フィールドの値がNULL値です: {logReportMsg}");
#endif
                return GetDefaultValue<T>();
            }

            object value = reader.GetValue(ordinal);

            //// NULLの二重チェック：理由：DBNull.Valueが取得された場合、Convert.ChangeType(value, typeof(T)) に渡すと InvalidCastExceptionが発生、かつ意図せずDBNull.ValueがConvert.ChangeTypeに渡る可能性があるため。
            if (value == DBNull.Value)
            {
                return GetDefaultValue<T>();
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new CustomDbSQLException($"DBからのフィールドの値のデータ型を変換するとき型変換エラーが発生: フィールド名: {columnName}, 期待値: {typeof(T).Name}, 実際の値: {value} : {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            string errRpt = $"フィールド名: {columnName} Entityタイプ: {typeof(T).Name} :内容: {ex.Message}";
            if ((ex is CustomNullException) || (ex is CustomDbSQLException))
            {
                throw;
            }

            if (ex is SqlException)
            {
                HLog.Err($"DBからのフィールドの値の取得に失敗しました。: {errRpt}");
                throw;
            }
            throw new CustomDbSQLException($"DBからのフィールドの値の取得時に予期しないエラーが発生しました。: {errRpt}");
        }
    }

    public static T GetDefaultValue<T>()
    {
        Type type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        return type switch
        {
            Type t when t == typeof(string) => (T)(object)string.Empty,
            Type t when t == typeof(int) => (T)(object)0,
            Type t when t == typeof(byte) => (T)(object)(byte)0,
            Type t when t == typeof(bool) => (T)(object)false,
            Type t when t == typeof(DateTime) => (T)(object)DateTime.MinValue,
            _ => throw new CustomDbSQLException($"想定外の型: {typeof(T).Name}")
        };
    }





}
