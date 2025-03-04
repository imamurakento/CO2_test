using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.aFunc.GoodCurrency;

internal class GoodNullObject : IGood
{
    public bool CreateC(ECurrency eCurrency)
    {
        throw new NotImplementedException();
    }

    public bool DeleteC(int targetID)
    {
        throw new NotImplementedException();
    }

    public List<ECurrency> LoadC()
    {
        throw new NotImplementedException();
    }

    public List<EGood> LoadG()
    {
        throw new NotImplementedException();
    }

    public ECurrency ReadC(int targetID)
    {
        throw new NotImplementedException();
    }

    public bool UpdateC(ECurrency eCurrency)
    {
        throw new NotImplementedException();
    }
}
