using CO2.Platforms.Windows;
using CommunityToolkit.Maui.Behaviors;

namespace CO2.At.Src.bDomain.Helpers;
public static class CursorIconHelper
{
    public enum CursorIcon
    {
        Wait,
        Hand,
        Arrow,
        IBeam,
        Cross,
        SizeAll,
    }

    public static void ApplyCursorToAllButtons(VisualElement rootElement, CursorIcon cursor, IMauiContext mauiContext)
    {
        if (rootElement == null || mauiContext == null)
        {
            throw new ArgumentNullException();
        }

        foreach (var child in rootElement.GetVisualTreeDescendants())
        {
            if (child is Button button)
            {
                // Handlerがすでに存在する場合
                if (button.Handler?.MauiContext is IMauiContext context)
                {
                    button.SetCustomCursor(cursor, context);
                }
                else
                {
                    // Handlerがまだ生成されていない場合
                    button.HandlerChanged += (s, e) =>
                    {
                        if (button.Handler?.MauiContext is IMauiContext handlerContext)
                        {
                            button.SetCustomCursor(cursor, handlerContext);
                        }
                    };
                }
            }
        }
    }


    public static IEnumerable<VisualElement> GetVisualTreeDescendants(this VisualElement element)
    {
        if (element is not IVisualTreeElement visualTreeElement)
        {
            yield break;
        }
        foreach (var child in visualTreeElement.GetVisualChildren())
        {
            if (child is VisualElement visualChild)
            {
                yield return visualChild;

                // 再帰的に子要素を探索
                foreach (var descendant in visualChild.GetVisualTreeDescendants())
                {
                    yield return descendant;
                }
            }
        }
    }


    public static void ApplyAnimationToAllButtons(VisualElement? root)
    {
        if (root == null)
        {
            return;
        }

        // IVisualTreeElement を使って子要素を取得
        if (root is IVisualTreeElement visualTreeElement)
        {
            foreach (var child in visualTreeElement.GetVisualChildren())
            {
                if (child is VisualElement visualChild)
                {
                    if (visualChild is Button button)
                    {
                        // アニメーションビヘイビアを追加
                        var animationBehavior = new AnimationBehavior
                        {
                            EventName = "Clicked",
                            AnimationType = new ScaleAnimation
                            {
                                Easing = Easing.Linear,
                                Length = 100,
                            },
                        };
                        button.Behaviors.Add(animationBehavior);
                    }

                    // 再帰的に子要素に処理を適用
                    ApplyAnimationToAllButtons(visualChild);
                }
            }
        }
    }
}
