using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.bDomain.aLogics;

namespace CO2.At.Src.bDomain.Helpers;

internal class VmToViewMsg
{
    public event Func<string, Task<bool>>? EvtQuestionRequested; // 「はい」「いいえ」の選択を受け取る
    public event Func<string, Task>? EvtConfirmRequested; // 確認メッセージ（選択結果の通知）

    private Page _TargetPage;

    public async Task DispConfirmMsg(string message)
    {
        if (_TargetPage != null)
        {
            await _TargetPage.Dispatcher.DispatchAsync(async () =>
            {
                await _TargetPage.DisplayAlert("情報", message, "OK");
            });
        }
    }

    public async Task<bool> DispQuestionMsg(string message)
    {
        if (_TargetPage != null)
        {
            return await _TargetPage.Dispatcher.DispatchAsync(async () =>
            {
                return await _TargetPage.DisplayAlert("確認", message, "はい", "いいえ");
            });
        }
        return false; // デフォルトは「いいえ」
    }

    public void SetTargetPage(Page page)
    {
        _TargetPage = page;
    }

    //public void RegisterEvtConfirm(Func<string, Task> handler)
    //{
    //    EvtConfirmRequested += handler;
    //}

    //public void UnregisterEvtConfirm(Func<string, Task> handler)
    //{
    //    EvtConfirmRequested -= handler;
    //}

    //public void RegisterEvtQuestion(Func<string, Task<bool>> handler)
    //{
    //    EvtQuestionRequested += handler;
    //}

    //public void UnregisterEvtQuestion(Func<string, Task<bool>> handler)
    //{
    //    EvtQuestionRequested -= handler;
    //}

    //ToDo:こっちだとView側の実装が不要だが、呼び出し元の画面が隠れてしまう。
    //public async Task DispConfirmMsg(string message)
    //{
    //    if (Application.Current?.MainPage != null)
    //    {
    //        await Application.Current.MainPage.DisplayAlert("情報", message, "OK");
    //    }
    //}

    ////ToDo:こっちだとView側の実装が必要となる
    //public async Task DispConfirmMsg(string message)
    //{
    //    await EvtConfirmRequested?.Invoke(message);
    //}

    //ToDo:こっちだとView側の実装が不要だが、呼び出し元の画面が隠れてしまう。
    //public async Task DispConfirmMsg(string message)
    //{

    //    var currentPage = GetCurrentPage();
    //    if (currentPage != null)
    //    {
    //        await currentPage.Dispatcher.DispatchAsync(async () =>
    //        {
    //            await currentPage.DisplayAlert("情報", message, "OK");
    //        });
    //    }

    //}

    //private Page? GetCurrentPage()
    //{
    //    return Application.Current?.MainPage?.Navigation?.NavigationStack.LastOrDefault()
    //           ?? Application.Current?.MainPage;
    //}

    /// <summary>
    /// 現在表示されている `Page` を取得
    /// </summary>
    //private Page? GetCurrentPage()
    //{
    //    if (Application.Current?.MainPage is Shell shell)
    //    {
    //        return shell.CurrentPage;
    //    }
    //    else if (Application.Current?.MainPage is NavigationPage navigationPage)
    //    {
    //        return navigationPage.Navigation.NavigationStack.LastOrDefault();
    //    }
    //    else if (Application.Current?.MainPage is TabbedPage tabbedPage)
    //    {
    //        return tabbedPage.CurrentPage;
    //    }
    //    else if (Application.Current?.MainPage is FlyoutPage flyoutPage)
    //    {
    //        return flyoutPage.Detail;
    //    }
    //    return Application.Current?.MainPage;
    //}

    //public static void SetMainPage(Page newPage)
    //{
    //    if (Application.Current != null)
    //    {
    //        Application.Current.MainPage = newPage;
    //    }
    //}
    /// <summary>
    /// 「はい」「いいえ」メッセージを表示し、結果を返す
    /// </summary>
    //public async Task<bool> DispQuestionMsg(string message)
    //{
    //    if (Application.Current?.MainPage != null)
    //    {
    //        return await Application.Current.MainPage.DisplayAlert("確認", message, "はい", "いいえ");
    //    }
    //    return false; // デフォルトは「いいえ」
    //}

    //public async Task<bool> DispQuestionMsg(string message)
    //{
    //    if (EvtQuestionRequested != null)
    //    {
    //        return await EvtQuestionRequested.Invoke(message);
    //    }
    //    return false; // デフォルトは「いいえ」
    //}

}
