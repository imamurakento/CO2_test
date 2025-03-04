using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Layouts;
using static CO2.At.Src.bDomain.Helpers.ViewBox;

namespace CO2.At.Src.bDomain.Helpers;

public class ViewBox : Layout
{

    public static readonly BindableProperty StretchProperty =
        BindableProperty.Create(nameof(Stretch), typeof(ViewBoxStretch), typeof(ViewBox), ViewBoxStretch.Uniform, propertyChanged: HandlePropertyChanged);

    public static readonly BindableProperty StretchDirectionProperty =
        BindableProperty.Create(nameof(StretchDirection), typeof(ViewBoxStretchDirection), typeof(ViewBox), ViewBoxStretchDirection.Both, propertyChanged: HandlePropertyChanged);

    public enum ViewBoxStretch
    {
        None,
        Fill,
        Uniform,
        UniformToFill,
    }

    public enum ViewBoxStretchDirection
    {
        UpOnly,
        DownOnly,
        Both,
    }

    public ViewBoxStretch Stretch
    {
        get => (ViewBoxStretch)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }

    public ViewBoxStretchDirection StretchDirection
    {
        get => (ViewBoxStretchDirection)GetValue(StretchDirectionProperty);
        set => SetValue(StretchDirectionProperty, value);
    }
    protected override ILayoutManager CreateLayoutManager() => new ViewBoxLayoutManager(this);


    private static void HandlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ViewBox viewBox)
        {
            viewBox.InvalidateMeasure();
        }
    }


}

public class ViewBoxLayoutManager : ILayoutManager
{

    private readonly ViewBox viewBox;
    public ViewBoxLayoutManager(ViewBox viewBox)
    {
        this.viewBox = viewBox;
    }


    private View? InternalChild => this.viewBox.Children.FirstOrDefault() as View;

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        Size constraint = new (widthConstraint, heightConstraint);
        View? child = InternalChild;
        Size parentSize = new ();

        if (child is not null)
        {
            child.Measure(double.PositiveInfinity, double.PositiveInfinity);
            //child.Measure(widthConstraint, heightConstraint);
            Size childSize = child.DesiredSize;

            Size scalefac = ComputeScaleFactor(constraint, childSize, this.viewBox.Stretch, this.viewBox.StretchDirection);

            parentSize.Width = scalefac.Width * childSize.Width;
            parentSize.Height = scalefac.Height * childSize.Height;
        }

        return parentSize;
    }

    public Size ArrangeChildren(Rect bounds)
    {
        Size arrangeSize = bounds.Size;
        View? child = InternalChild;

        if (child is not null)
        {
            Size childSize = child.DesiredSize;
            Size scalefac = ComputeScaleFactor(arrangeSize, childSize, this.viewBox.Stretch, this.viewBox.StretchDirection);

            arrangeSize.Width = scalefac.Width * childSize.Width;
            arrangeSize.Height = scalefac.Height * childSize.Height;

            child.AnchorX = 0;
            child.AnchorY = 0;
            child.ScaleX = scalefac.Width;
            child.ScaleY = scalefac.Height;

            double yOffset = child.VerticalOptions.Alignment switch
            {
                LayoutAlignment.Start => 0,
                LayoutAlignment.End => bounds.Height - arrangeSize.Height,
                _ => (bounds.Height - arrangeSize.Height) / 2.0,
            };
            double xOffset = child.HorizontalOptions.Alignment switch
            {
                LayoutAlignment.Start => 0,
                LayoutAlignment.End => bounds.Width - arrangeSize.Width,
                _ => (bounds.Width - arrangeSize.Width) / 2.0,
            };

            ((IView)child).Arrange(new Rect(new Point(xOffset, yOffset), child.DesiredSize));
        }



        return arrangeSize;
    }

    internal static Size ComputeScaleFactor(Size availableSize, Size contentSize, ViewBox.ViewBoxStretch stretch, ViewBox.ViewBoxStretchDirection stretchDirection)
    {
        double scaleX = 1.0;
        double scaleY = 1.0;

        bool isConstrainedWidth = !double.IsPositiveInfinity(availableSize.Width);
        bool isConstrainedHeight = !double.IsPositiveInfinity(availableSize.Height);

        if ((stretch == ViewBox.ViewBoxStretch.Uniform || stretch == ViewBox.ViewBoxStretch.UniformToFill || stretch == ViewBox.ViewBoxStretch.Fill)
            && (isConstrainedWidth || isConstrainedHeight))
        {

            // 制約のある次元でスケールを計算
            scaleX = isConstrainedWidth ? (contentSize.Width == 0 ? 0 : availableSize.Width / contentSize.Width) : 1.0;
            scaleY = isConstrainedHeight ? (contentSize.Height == 0 ? 0 : availableSize.Height / contentSize.Height) : 1.0;
            // 制約のない次元にスケールを伝播
            if (!isConstrainedWidth && isConstrainedHeight)
            {
                scaleX = scaleY; // Y方向の制約をXに適用
            }
            else if (!isConstrainedHeight && isConstrainedWidth)
            {
                scaleY = scaleX; // X方向の制約をYに適用
            }
            else
            {
                switch (stretch)
                {
                    case ViewBoxStretch.Uniform:
                        double minScale = Math.Min(scaleX, scaleY);
                        scaleX = scaleY = minScale;
                        break;
                    case ViewBoxStretch.UniformToFill:
                        double maxScale = Math.Max(scaleX, scaleY);
                        scaleX = scaleY = maxScale;
                        break;
                    case ViewBoxStretch.Fill:
                        break;
                }
            }

            switch (stretchDirection)
            {
                case ViewBox.ViewBoxStretchDirection.UpOnly:
                    if (scaleX < 1.0)
                    {
                        scaleX = 1.0;
                    }

                    if (scaleY < 1.0)
                    {
                        scaleY = 1.0;
                    }
                    break;
                case ViewBox.ViewBoxStretchDirection.DownOnly:
                    if (scaleX > 1.0)
                    {
                        scaleX = 1.0;
                    }
                    if (scaleY > 1.0)
                    {
                        scaleY = 1.0;
                    }
                    break;
            }
        }
        return new Size(scaleX, scaleY);
    }
}
