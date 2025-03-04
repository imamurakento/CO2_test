using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;


public partial class ContactDetails : InputValidatorBase
{
    [ObservableProperty]
    private ObservableCollection<string>? _contactDetailsList;

    [ObservableProperty]
    private string _selectedContactDetails;

    public ContactDetails()
    {

        ContactDetailsList =[
                "自宅電話",
                "携帯電話",
                "勤務先電話"];
        SelectedContactDetails = "自宅電話";
    }
}
