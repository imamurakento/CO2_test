using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.eValueObject;

namespace CO2.At.Src.aFunc.Employee;
public interface IEmp
{
    public bool ChkLogin(string userName, string password);

    public List<EParent> LoadP();

    public List<EChild> LoadC();

    public List<EEmp> LoadE();

    public bool CreateP(EParent eOrganizationParent);

    public EParent? ReadP(int targetID);

    public bool UpdateP(EParent eOrganizationParent);

    public bool DeleteP(int targetID);

    public int GetMaxOrganizationId();
}
