using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;



using Windows.UI.Core;
using static CO2.At.Src.bDomain.Helpers.CursorIconHelper;

namespace CO2.Platforms.Windows
{
    // カーソル変更に関する拡張メソッドを提供する静的クラス
    public static class CursorExtensions
    {
        // VisualElement に対してカスタムカーソルを設定するメソッド
        public static void SetCustomCursor(this VisualElement visualElement, CursorIcon cursor, IMauiContext? mauiContext)
        {
            // 引数の null チェック。mauiContext が null の場合は例外をスロー
            ArgumentNullException.ThrowIfNull(mauiContext);

            // VisualElement をネイティブ UI 要素 (UIElement) に変換
            UIElement view = visualElement.ToPlatform(mauiContext);

            // マウスが要素に入った時のイベントを登録
            view.PointerEntered += ViewOnPointerEntered;

            // マウスが要素から出た時のイベントを登録
            view.PointerExited += ViewOnPointerExited;

            // マウスが要素から出た際の処理
            void ViewOnPointerExited(object sender, PointerRoutedEventArgs e)
            {
                // カーソルをデフォルトの矢印 (Arrow) に変更
                view.ChangeCursor(InputCursor.CreateFromCoreCursor(new CoreCursor(GetCursor(CursorIcon.Arrow), 1)));
            }

            // マウスが要素に入った際の処理
            void ViewOnPointerEntered(object sender, PointerRoutedEventArgs e)
            {
                // 指定されたカーソルアイコンに変更
                view.ChangeCursor(InputCursor.CreateFromCoreCursor(new CoreCursor(GetCursor(cursor), 1)));
            }
        }

        // UIElement のカーソルを変更するための拡張メソッド
        public static void ChangeCursor(this UIElement uiElement, InputCursor cursor)
        {
            // UIElement の "ProtectedCursor" プロパティに新しいカーソルを設定
            Type type = typeof(UIElement);
            type.InvokeMember(
                "ProtectedCursor",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                null,
                uiElement,
                new object[] { cursor });
        }

        // CursorIcon を CoreCursorType に変換するメソッド
        public static CoreCursorType GetCursor(CursorIcon cursor)
        {
            // 各 CursorIcon に対応する CoreCursorType を返す
            return cursor switch
            {
                CursorIcon.Hand => CoreCursorType.Hand,
                CursorIcon.IBeam => CoreCursorType.IBeam,
                CursorIcon.Cross => CoreCursorType.Cross,
                CursorIcon.Arrow => CoreCursorType.Arrow,
                CursorIcon.SizeAll => CoreCursorType.SizeAll,
                CursorIcon.Wait => CoreCursorType.Wait,
                _ => CoreCursorType.Arrow, // デフォルトは Arrow
            };
        }
    }

}
