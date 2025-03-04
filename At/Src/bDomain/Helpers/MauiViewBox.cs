using Microsoft.Maui.Controls.Compatibility;

namespace CO2.At.Src.bDomain.Helpers;

public class MauiViewBox : Layout<View>
{
    public enum StretchMode
    {
        None,
        Fill,
        Uniform,
        UniformToFill,
    }

    public static readonly BindableProperty StretchProperty =
    BindableProperty.Create(nameof(Stretch), typeof(StretchMode), typeof(ViewBox), StretchMode.Uniform, propertyChanged: OnStretchChanged);

    public StretchMode Stretch
    {
        get => (StretchMode)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }

    private static void OnStretchChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as MauiViewBox)?.InvalidateMeasure();
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        if (Children.Count == 0)
        {
            return new Size(0, 0);
        }

        View content = Children[0];
        SizeRequest contentSize = content.Measure(double.PositiveInfinity, double.PositiveInfinity);

        double scaleX = widthConstraint / contentSize.Request.Width;
        double scaleY = heightConstraint / contentSize.Request.Height;
        double scale = 1;

        switch (Stretch)
        {
            case StretchMode.Fill:
                scale = Math.Max(scaleX, scaleY);
                break;
            case StretchMode.Uniform:
                scale = Math.Min(scaleX, scaleY);
                break;
            case StretchMode.UniformToFill:
                scale = Math.Max(scaleX, scaleY);
                break;
        }

        double newWidth = contentSize.Request.Width * scale;
        double newHeight = contentSize.Request.Height * scale;

        return new Size(newWidth, newHeight);
    }

    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        if (Children.Count == 0)
        {
            return;
        }

        View content = Children[0];
        SizeRequest contentSize = content.Measure(double.PositiveInfinity, double.PositiveInfinity);
        double scaleX = width / contentSize.Request.Width;
        double scaleY = height / contentSize.Request.Height;
        double scale = 1;

        switch (Stretch)
        {
            case StretchMode.Fill:
                scale = Math.Max(scaleX, scaleY);
                break;
            case StretchMode.Uniform:
                scale = Math.Min(scaleX, scaleY);
                break;
            case StretchMode.UniformToFill:
                scale = Math.Max(scaleX, scaleY);
                break;
        }

        double newWidth = contentSize.Request.Width * scale;
        double newHeight = contentSize.Request.Height * scale;
        double xPos = (width - newWidth) / 2;
        double yPos = (height - newHeight) / 2;

        LayoutChildIntoBoundingRegion(content, new Rect(xPos, yPos, newWidth, newHeight));
    }
}
