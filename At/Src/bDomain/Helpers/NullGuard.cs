using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;

namespace CO2.At.Src.bDomain.Helpers;

public static class NullGuard
{

    public static void NullThrow(object o, string message)
    {
        if (o == null)
        {
            throw new CustomNullException(message);
        }
    }

    public static void ListItemNullThrow<T>(IList<T>? argument, object item, string message)
    {
        if (argument == null || item == null)
        {
            throw new CustomNullException(message);
        }
    }

    //public static void ArgumentNotNull(object? argument, string argumentName)
    //{
    //    if (argument == null)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(IEnumerable<T>? argument, string argumentName)
    //{
    //    if (argument == null || !argument.Any())
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(ICollection<T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(IReadOnlyCollection<T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(IReadOnlyList<T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(IReadOnlyDictionary<T, T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(IReadOnlySet<T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}

    //public static void ArgumentNotNullOrEmpty<T>(ISet<T>? argument, string argumentName)
    //{
    //    if (argument == null || argument.Count == 0)
    //    {
    //        throw new ArgumentNullException(argumentName);
    //    }
    //}


}
