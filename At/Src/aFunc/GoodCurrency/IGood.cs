using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Entities;

namespace CO2.At.Src.aFunc.GoodCurrency;

public interface IGood
{
    public List<ECurrency> LoadC();

    public List<EGood> LoadG();

    public bool CreateC(ECurrency eCurrency);

    public ECurrency ReadC(int targetID);

    public bool UpdateC(ECurrency eCurrency);

    public bool DeleteC(int targetID);
}
