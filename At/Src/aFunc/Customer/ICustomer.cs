using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Entities;

namespace CO2.At.Src.aFunc.Customer;

public interface ICustomer
{
    public List<ECustomer> Load();
    public bool Create(ECustomer eCustomer);

    public ECustomer? Read(int target_number);

    public bool Update(ECustomer eCustomer);

    public bool Delete(int target_number);

    public int GetNewID();
    public List<ECustomer>? SearchByVowel(string vowelKindName);
    public List<ECustomer>? PartialSearch(string fullName, string furigana, string personalPhone, string companyPhone);

    public List<ECustomer> LoadDeleteCustomers();
}
