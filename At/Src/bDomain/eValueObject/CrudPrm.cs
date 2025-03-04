using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.eValueObject;

internal class CrudPrm
{
#pragma warning disable SA1310 // 命名スタイル
#pragma warning disable IDE1006 // 命名スタイル
    public readonly int CRUD_Kind;
#pragma warning restore IDE1006 // 命名スタイル
#pragma warning disable SA1310 // 命名スタイル

    public CrudPrm(int crudKind)
    {
        CRUD_Kind = crudKind;
    }

}
