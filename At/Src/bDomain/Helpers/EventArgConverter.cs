using System.Globalization;

namespace CO2.At.Src.bDomain.Helpers;

#pragma warning disable CS8767

public class TextChangedEventArgsConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TextChangedEventArgs args)
        {
            return args.NewTextValue;
        }
        return null;
        }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
    throw new NotImplementedException();
    }
}

public class UnfocusedEventArgsConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is FocusEventArgs args && args.VisualElement is Entry entry)
        {
            // Entry の Text プロパティを返します。
            return entry.Text;
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

////Completeは検出できないので、コードビハインド側でしないと現状対応できない。理由は不明。必須ではないのでCompleteは現時点では実装しない

public class CompletedEventArgsConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // sender（コントロール）として Entry を取得する
        if (value is Entry entry)
        {
            return entry.Text;
        }

        return null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

#pragma warning disable CS8767
