using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.bDomain.eValueObject;

public class LoginUser
{
    private readonly string _password;

#pragma warning disable IDE0290 // プライマリ コンストラクターの使用
    public LoginUser(string userName, string passWord)
#pragma warning restore IDE0290 // プライマリ コンストラクターの使用
    {
        UserName = userName;
        _password = passWord;
    }

    public string UserName { get; }


}
