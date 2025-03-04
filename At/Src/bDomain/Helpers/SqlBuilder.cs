using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.Helpers;

internal class SqlBuilder
{

    //まずは簡易版。部分一致検索検索用途。
    public string BuildPartialMatchQuery(string fullName, string furigana, string personalPhone, string companyPhone)
    {
        return $@"
            SELECT * FROM Customer 
            WHERE delete_flag = 0 
            AND full_name LIKE @fullName
            AND furigana LIKE @furigana
            AND personal_phone_number LIKE @personalPhone
            AND employer_phone_number LIKE @companyPhone";
    }

}
