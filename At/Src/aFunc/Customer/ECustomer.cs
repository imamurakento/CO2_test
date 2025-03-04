using CO2.At.Src.aFunc.Customer.Parts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer;


public partial class ECustomer : ObservableObject
{

    [ObservableProperty]
    private int _customerNumber=0;
    [ObservableProperty]
    private string _furigana = string.Empty;
    [ObservableProperty]
    private string _fullName = string.Empty;
    [ObservableProperty]
    private Gender _gender;
    [ObservableProperty]
    private Birthday _birthday;
    [ObservableProperty]
    private EmployeeInCharge _empCharge;
    [ObservableProperty]
    private ContactDetails _contactDetails;
    [ObservableProperty]
    private string _empName = string.Empty;
    [ObservableProperty]
    private string _empAddress2 = string.Empty;
    [ObservableProperty]
    private PhoneNumber _empPhoneNumber;
    [ObservableProperty]
    private Address _empAddress;
    [ObservableProperty]
    private PhoneNumber _psnPhoneNumber;
    [ObservableProperty]
    private PhoneNumber _psnFaxNumber;
    [ObservableProperty]
    private PhoneNumber _psnMobileNumber;
    [ObservableProperty]
    private Address _psnAddress;
    [ObservableProperty]
    private string _psnAddress2 = string.Empty;
    [ObservableProperty]
    private BankAccounts _psnBankAccounts;
    [ObservableProperty]
    private SubmissionCertificate _submissionCertificate;

    public ECustomer()
    {
        _gender = new ();
        _birthday = new ();
        _empCharge = new ();
        _contactDetails = new ();
        _empPhoneNumber = new ();
        _empAddress = new ();
        _psnPhoneNumber = new ();
        _psnFaxNumber = new ();
        _psnMobileNumber = new ();
        _psnAddress = new ();
        _psnBankAccounts = new ();
        _submissionCertificate = new ();
    }

    public int AutoId { get; set; } = 0;

    //ToDo:バインド漏れしているのでチェック
    public byte MailingDestinationType { get; set; } = 0;

    //ToDo:バインド漏れしているかチェック
    public byte ContactKind { get; set; } = 0;

    public int RepresentativeId { get; set; } = 0;

    public bool DeleteFlag { get; set; } = false;

    public DateTime AutoUpdatedAt { get; set; } = DateTime.MaxValue;

    public DateTime AutoCreatedAt { get; set; } = DateTime.MaxValue;

    public DateTime AutoEffectiveStartDate { get; set; } = DateTime.MaxValue;

    public DateTime AutoEffectiveEndDate { get; set; } = DateTime.MaxValue;


    public void Set(
            int customerNumber,
            string furigana,
            string fullName,
            byte gender,
            DateTime birthDate,
            byte mailingDestinationType,
            byte contactKind,
            int representativeId,
            string employerName,
            string employerPhoneNumber,
            string employerPostalCode,
            string employerAddress,
            string employerAddress2,
            string personalPhoneNumber,
            string personalFaxNumber,
            string personalMobileNumber,
            string personalPostalCode,
            string personalAddress,
            string personalAddress2,
            byte notifymattersDocumentsKind,
            string notifymattersCertificationNumber,
            string notifymattersBankName,
            string notifymattersBranchName,
            byte notifymattersAccountKind,
            string notifymattersAccountNumber,
            bool deleteFlag,
            DateTime autoUpdatedAt,
            DateTime autoCreatedAt,
            DateTime autoEffectiveStartDate,
            DateTime autoEffectiveEndDate)
    {
        CustomerNumber = customerNumber;
        Furigana = furigana;
        FullName = fullName;
        Gender.SetGender(gender);
        Birthday.SetBirthday(birthDate);
        MailingDestinationType = mailingDestinationType;
        ContactKind = contactKind;
        RepresentativeId = representativeId;
        EmpName = employerName;
        EmpPhoneNumber.SetPhoneNumber(employerPhoneNumber);
        EmpAddress.SetPostalCode(employerPostalCode);
        EmpAddress.SetAddress(employerAddress);
        EmpAddress2 = employerAddress2;
        PsnPhoneNumber.SetPhoneNumber(personalPhoneNumber);
        PsnFaxNumber.SetPhoneNumber(personalFaxNumber);
        PsnMobileNumber.SetPhoneNumber(personalMobileNumber);
        PsnAddress.SetPostalCode(personalPostalCode);
        PsnAddress.SetAddress(personalAddress);
        PsnAddress2 = personalAddress2;
        SubmissionCertificate.SetDocumentKind(notifymattersDocumentsKind);
        SubmissionCertificate.SetCertificationNumber(notifymattersCertificationNumber);
        PsnBankAccounts.SetBankName(notifymattersBankName);
        PsnBankAccounts.SetBranchName(notifymattersBranchName);
        PsnBankAccounts.SetAccountKind(notifymattersAccountKind);
        PsnBankAccounts.SetAccountNumber(notifymattersAccountNumber);
        DeleteFlag = deleteFlag;
        AutoUpdatedAt = autoUpdatedAt;
        AutoCreatedAt = autoCreatedAt;
        AutoEffectiveStartDate = autoEffectiveStartDate;
        AutoEffectiveEndDate = autoEffectiveEndDate;
    }



}////class