using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class SubmissionCertificate : InputValidatorBase, IInputValidator
{
    [ObservableProperty]
    private byte _selectedDocumentKind = 0;
    [ObservableProperty]
    private string _certificationNumber = string.Empty;

    public int GetDocumentKind() => SelectedDocumentKind;
    public void SetDocumentKind(byte documentIndex) => SelectedDocumentKind = (byte)(Convert.ToInt32(documentIndex) - 1);

    public string GetCertificationNumber() => CertificationNumber;

    public void SetCertificationNumber(string certificationNumber) => CertificationNumber = certificationNumber;

    public bool IsValidated()
    {
        throw new NotImplementedException();
    }
}
