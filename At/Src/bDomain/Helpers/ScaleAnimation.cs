using CommunityToolkit.Maui.Animations;

namespace CO2.At.Src.bDomain.Helpers;

public class ScaleAnimation : BaseAnimation
{
    public override async Task Animate(VisualElement view, CancellationToken token)
    {
        await view.ScaleTo(0.8, Length, Easing);
        await view.ScaleTo(1, Length, Easing);
    }
}