using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;

#if UNIT_TEST

namespace CO2.At.Src.Tests.xUnit;

public static class BindTest
{
    private static int _fullNameNumber = 0;
    private static bool _isStop = false;
    private static Vw1VM _vw1MdlCustomer = GIc.GetVw1VM();

    // バインドテスト開始
    public static async void OnStartButtonClicked()
    {
        _isStop = false;
        _fullNameNumber = 0;
        while (!_isStop)
        {
            _fullNameNumber++;
            //_vw1MdlCustomer.SetFullName(_fullNameNumber.ToString());
            await Task.Delay(1000);
        }
    }

    // バインドテストストップ
    public static void OnStopButtonClicked()
    {
        _isStop = true;
    }
}

#endif
