using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CO2.At.Src.bDomain.Helpers;

public static class ExceptionHandler
{
    public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            HLog.Err(" CurrentDomain_UnhandledException Called");
            HLog.Err($"Explain1:{e.ToString()}");
            HLog.Err($"Explain2:{e.ExceptionObject.ToString()}");
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }

    ////CurrentDomain_UnhandledExceptionとタイミングがかぶるので現時点では必要ない。
    //public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    //{
    //    try
    //    {
    //        HLog.Err("OnUnhandledException Called");
    //    }
    //    catch (Exception ex)
    //    {
    //        HLog.Except(ex);
    //    }
    //}

    public static void LogExceptionDetails(Exception ex)
    {
        string catMsg;

        catMsg = $"例外の種類: {ex.GetType().FullName}";
        HLog.Err(catMsg);
        catMsg = $"内容: {ex.Message}";
        HLog.Err(catMsg);
        catMsg = $"スタックトレース: {ex.StackTrace}";
        HLog.Err(catMsg);

        if (ex.InnerException != null)
        {
            catMsg = "$LogExceptionDetails:内部例外の種類: {ex.InnerException.GetType().FullName} 内部例外メッセージ: {ex.InnerException.Message} 内部例外スタックトレース: {ex.InnerException.StackTrace}";
            HLog.Err(catMsg);
        }

        catMsg = $"種類:{ex.GetType().ToString()}";
        HLog.Err(catMsg);

        //// 例外の型に応じて追加情報を判定
        if (ex is ArgumentOutOfRangeException)
        {
            HLog.Err("ArgumentOutOfRangeException例外発生");
        }
        else if (ex is System.SystemException)
        {
            ESystemException(ex);
        }
        else if (ex is System.Diagnostics.Eventing.Reader.EventLogException)
        {
            EEventLogException(ex);
        }
        else if (ex is System.Net.Mail.SmtpException)
        {
            ESmtpException(ex);
        }
        else if (ex is System.ApplicationException)
        {
            EApplicationException(ex);
        }
        else if (ex is System.TimeZoneNotFoundException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Collections.Generic.KeyNotFoundException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.ComponentModel.DataAnnotations.ValidationException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Configuration.SettingsPropertyIsReadOnlyException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Configuration.SettingsPropertyNotFoundException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Configuration.SettingsPropertyWrongTypeException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Diagnostics.Tracing.EventSourceException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.IO.IsolatedStorage.IsolatedStorageException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Net.Http.HttpRequestException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Runtime.CompilerServices.RuntimeWrappedException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Runtime.Serialization.InvalidDataContractException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Threading.BarrierPostPhaseException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Threading.LockRecursionException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Threading.Tasks.TaskSchedulerException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Xml.XmlException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is System.Xml.XPath.XPathException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is Microsoft.CSharp.RuntimeBinder.RuntimeBinderInternalCompilerException)
        {
            ENoSubClassException(ex);
        }
        else if (ex is Microsoft.VisualBasic.FileIO.MalformedLineException)
        {
            ENoSubClassException(ex);
        }
        else
        {
            ENoSubClassException(ex);

            HLog.Err("予期しない例外発生: " + ex.GetType().FullName + " - " + ex.Message);
        }

    }

    public static void ESubArithmeticException(Exception ex)
    {
        if (ex is System.DivideByZeroException)
        {
            HLog.Err("There is an attempt to divide an integral or decimal value by zero.");
        }
        if (ex is NotFiniteNumberException)
        {
            HLog.Err("A floating-point value is positive infinity, negative infinity, or Not-a-Number (NaN).");
        }
        else if (ex is OverflowException)
        {
            HLog.Err("An arithmetic, casting, or conversion operation in a checked context results in an overflow.");
        }
        else
        {
            HLog.Err("The exception that is thrown for errors in an arithmetic, casting, or conversion operation.");
        }
    }

    public static void ENoSubClassException(Exception ex)
    {
        if (ex is TimeZoneNotFoundException)
        {
            HLog.Err("a time zone cannot be found.");
        }
        else if (ex is KeyNotFoundException)
        {
            HLog.Err("the key specified for accessing an element in a collection does not match any key in the collection.");
        }
        else if (ex is System.ComponentModel.DataAnnotations.ValidationException)
        {
            HLog.Err("Represents the exception that occurs during validation of a data field when the System.ComponentModel.DataAnnotations.ValidationAttribute class is used.");
        }
        else if (ex is System.Configuration.SettingsPropertyIsReadOnlyException)
        {
            HLog.Err("Provides an exception for read-only System.Configuration.SettingsProperty objects.");
        }
        else if (ex is System.Configuration.SettingsPropertyNotFoundException)
        {
            HLog.Err("Provides an exception for System.Configuration.SettingsProperty objects that are not found.");
        }
        else if (ex is System.Configuration.SettingsPropertyWrongTypeException)
        {
            HLog.Err("Provides an exception that is thrown when an invalid type is used with a System.Configuration.SettingsProperty object.");
        }
        else if (ex is System.Diagnostics.Tracing.EventSourceException)
        {
            HLog.Err("an error occurs during event tracing for Windows (ETW).");
        }
        else if (ex is System.IO.IsolatedStorage.IsolatedStorageException)
        {
            HLog.Err("an operation in isolated storage fails.");
        }
        else if (ex is HttpRequestException)
        {
            HLog.Err("A base class for exceptions thrown by the System.Net.Http.HttpClient and System.Net.Http.HttpMessageHandler classes.");
        }
        else if (ex is System.Runtime.CompilerServices.RuntimeWrappedException)
        {
            HLog.Err("Wraps an exception that does not derive from the System.Exception class. This class cannot be inherited.");
        }
        else if (ex is System.Runtime.Serialization.InvalidDataContractException)
        {
            HLog.Err("the System.Runtime.Serialization.DataContractSerializer or System.Runtime.Serialization.NetDataContractSerializer encounters an invalid data contract during serialization and deserialization.");
        }
        else if (ex is BarrierPostPhaseException)
        {
            HLog.Err("the post-phase action of a System.Threading.Barrier fails");
        }
        else if (ex is LockRecursionException)
        {
            HLog.Err("recursive entry into a lock is not compatible with the recursion policy for the lock.");
        }
        else if (ex is TaskSchedulerException)
        {
            HLog.Err("Represents an exception used to communicate an invalid operation by a System.Threading.Tasks.TaskScheduler.");
        }
        else if (ex is System.Xml.XmlException)
        {
            HLog.Err("Returns detailed information about the last exception.");
        }
        else if (ex is System.Xml.XPath.XPathException)
        {
            HLog.Err("Provides an error occurs while processing an XPath expression.");
        }
        else if (ex is Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        {
            HLog.Err("Represents an error that occurs when a dynamic bind in the C# runtime binder is processed.");
        }
        else if (ex is Microsoft.CSharp.RuntimeBinder.RuntimeBinderInternalCompilerException)
        {
            HLog.Err("Represents an error that occurs when a dynamic bind in the C# runtime binder is processed.");
        }
        else if (ex is Microsoft.VisualBasic.FileIO.MalformedLineException)
        {
            HLog.Err("the Microsoft.VisualBasic.FileIO.TextFieldParser.ReadFields method cannot parse a row using the specified format.");
        }
    }

    [Obsolete]
    public static void ESystemException(Exception ex)
    {
        if (ex is AccessViolationException)
        {
            HLog.Err("there is an attempt to read or write protected memory.");
        }
        else if (ex is AggregateException)
        {
            HLog.Err("Represents one or more errors that occur during application execution.");
        }
        else if (ex is AppDomainUnloadedException)
        {
            HLog.Err("an attempt is made to access an unloaded application domain.");
        }
        else if (ex is ArgumentException)
        {
            HLog.Err("ArgumentException発生　親クラス:SystemException ログ文:one of the arguments provided to a method is not valid.");
            ESubArgumentException(ex);
        }
        else if (ex is ArithmeticException)
        {
            HLog.Err("ArithmeticException発生　親クラス:SystemException ログ文:The exception that is thrown for errors in an arithmetic, casting, or conversion operation.");
            ESubArithmeticException(ex);
        }
        else if (ex is System.Data.DataException)
        {
            HLog.Err("DataException発生　親クラス:SystemException ログ文:errors are generated using ADO.NET components.");
            ESubDataException(ex);
        }
        else if (ex is System.Data.SqlTypes.SqlTypeException)
        {
            HLog.Err("SqlTypeException発生　親クラス:SystemException ログ文:The base exception class for the System.Data.SqlTypes.");
            ESubSqlTypeException(ex);
        }
        else if (ex is FormatException)
        {
            HLog.Err("FormatException発生　親クラス:SystemException ログ文:the format of an argument is invalid, or when a composite format string is not well formed.");
            ESubFormatException(ex);
        }
        else if (ex is InvalidOperationException)
        {
            HLog.Err("InvalidOperationException発生　親クラス:SystemException ログ文:a method call is invalid for the object's current state.");
            ESubInvalidOperationException(ex);
        }
        else if (ex is IOException)
        {
            HLog.Err("IOException発生　親クラス:SystemException ログ文:an I/O error occurs.");
            ESubIOException(ex);
        }
        else if (ex is MemberAccessException)
        {
            HLog.Err("MemberAccessException発生　親クラス:SystemException ログ文:an attempt to access a class member fails.");
            ESubMemberAccessException(ex);
        }
        else if (ex is NotSupportedException)
        {
            HLog.Err("NotSupportedException発生　親クラス:SystemException ログ文:an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.");
            ESubNotSupportedException(ex);
        }
        else if (ex is OperationCanceledException)
        {
            HLog.Err("OperationCanceledException発生　親クラス:SystemException ログ文:The exception that is thrown in a thread upon cancellation of an operation that the thread was executing.");
            ESubOperationCanceledException(ex);
        }
        else if (ex is OutOfMemoryException)
        {
            HLog.Err("OutOfMemoryException発生　親クラス:SystemException ログ文:there is not enough memory to continue the execution of a program.");
            ESubOutOfMemoryException(ex);
        }
        else if (ex is System.Security.Cryptography.CryptographicException)
        {
            HLog.Err("CryptographicException発生　親クラス:SystemException ログ文:an error occurs during a cryptographic operation.");
            ESubCryptographicException(ex);
        }
        else if (ex is TimeoutException)
        {
            HLog.Err("TimeoutException発生　親クラス:SystemException ログ文:the time allotted for a process or operation has expired.");
            ESubTimeoutException(ex);
        }
        else if (ex is System.Transactions.TransactionException)
        {
            HLog.Err("TransactionException発生　親クラス:SystemException ログ文:you attempt to do work on a transaction that cannot accept new work.");
            ESubTransactionException(ex);
        }
        else if (ex is TypeLoadException)
        {
            HLog.Err("TypeLoadException発生　親クラス:SystemException ログ文:type-loading failures occur.");
            ESubTypeLoadException(ex);
        }
        else if (ex is UnauthorizedAccessException)
        {
            HLog.Err("UnauthorizedAccessException発生　親クラス:SystemException ログ文:the operating system denies access because of an I/O error or a specific type of security error.");
            ESubUnauthorizedAccessException(ex);
        }
        else if (ex is System.Xml.Schema.XmlSchemaException)
        {
            HLog.Err("XmlSchemaException発生　親クラス:SystemException ログ文:Returns detailed information about the schema exception.");
            ESubXmlSchemaException(ex);
        }
        else if (ex is System.Xml.Xsl.XsltException)
        {
            HLog.Err("XsltException発生　親クラス:SystemException ログ文:an error occurs while processing an XSLT transformation.");
            ESubXsltException(ex);
        }
        else if (ex is System.Configuration.ConfigurationException)
        {
            HLog.Err("ConfigurationException発生　親クラス:SystemException ログ文:a configuration system error has occurred.");
            ESubConfigurationException(ex);
        }
        else if (ex is System.Runtime.InteropServices.ExternalException)
        {
            HLog.Err("ExternalException発生　親クラス:SystemException ログ文:The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.");
            ESubExternalException(ex);
        }
        else if (ex is System.Security.Authentication.AuthenticationException)
        {
            HLog.Err("AuthenticationException発生　親クラス:SystemException ログ文:authentication fails for an authentication stream.");
            ESubAuthenticationException(ex);
        }
        else if (ex is AbandonedMutexException)
        {
            HLog.Err("one thread acquires a System.Threading.Mutex object that another thread has abandoned by exiting without releasing it.");
        }
        else if (ex is SemaphoreFullException)
        {
            HLog.Err("the System.Threading.Semaphore.Release method is called on a semaphore whose count is already at the maximum.");
        }
        else if (ex is SynchronizationLockException)
        {
            HLog.Err("a method requires the caller to own the lock on a given Monitor, and the method is invoked by a caller that does not own that lock.");
        }
        else if (ex is ThreadAbortException)
        {
            HLog.Err("a call is made to the System.Threading.Thread.Abort(System.Object) method. This class cannot be inherited.");
        }
        else if (ex is ThreadInterruptedException)
        {
            HLog.Err("a System.Threading.Thread is interrupted while it is in a waiting state.");
        }
        else if (ex is ThreadStartException)
        {
            HLog.Err("a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.");
        }
        else if (ex is ThreadStateException)
        {
            HLog.Err("a System.Threading.Thread is in an invalid System.Threading.Thread.ThreadState for the method call.");
        }
        else if (ex is ArrayTypeMismatchException)
        {
            HLog.Err("an attempt is made to store an element of the wrong type within an array.");
        }
        else if (ex is BadImageFormatException)
        {
            HLog.Err("the file image of a dynamic link library (DLL) or an executable program is invalid.");
        }
        else if (ex is CannotUnloadAppDomainException)
        {
            HLog.Err("an attempt to unload an application domain fails.");
        }
        else if (ex is ContextMarshalException)
        {
            HLog.Err("an attempt to marshal an object across a context boundary fails.");
        }
        else if (ex is DataMisalignedException)
        {
            HLog.Err("a unit of data is read from or written to an address that is not a multiple of the data size. This class cannot be inherited.");
        }
        else if (ex is System.Security.Principal.IdentityNotMappedException)
        {
            HLog.Err("Represents an exception for a principal whose identity could not be mapped to a known identity.");
        }
        else if (ex is Microsoft.SqlServer.Server.InvalidUdtException)
        {
            HLog.Err("Thrown when SQL Server or the ADO.NET System.Data.SqlClient provider detects an invalid user-defined type (UDT).");
        }
        else if (ex is System.Runtime.InteropServices.InvalidComObjectException)
        {
            HLog.Err("an invalid COM object is used.");
        }
        else if (ex is System.Runtime.InteropServices.InvalidOleVariantTypeException)
        {
            HLog.Err("the marshaler when it encounters an argument of a variant type that can not be marshaled to managed code.");
        }
        else if (ex is System.Runtime.InteropServices.MarshalDirectiveException)
        {
            HLog.Err("the marshaler when it encounters a System.Runtime.InteropServices.MarshalAsAttribute it does not support.");
        }
        else if (ex is System.Runtime.InteropServices.SafeArrayRankMismatchException)
        {
            HLog.Err("the rank of an incoming SAFEARRAY does not match the rank specified in the managed signature.");
        }
        else if (ex is System.Runtime.InteropServices.SafeArrayTypeMismatchException)
        {
            HLog.Err("the type of the incoming SAFEARRAY does not match the type specified in the managed signature.");
        }
        else if (ex is Exception)
        {
            HLog.Err("Represents errors that occur during application execution.To browse the .NET Framework source code for this type, see the Reference Source.");
        }
        else if (ex is ExecutionEngineException)
        {
            HLog.Err("there is an internal error in the execution engine of the common language runtime. This class cannot be inherited.");
        }
        else if (ex is System.Runtime.Serialization.SerializationException)
        {
            HLog.Err("an error occurs during serialization or deserialization.");
        }
        else if (ex is System.Security.SecurityException)
        {
            HLog.Err("a security error is detected.");
        }
        else if (ex is System.Security.VerificationException)
        {
            HLog.Err("the security policy requires code to be type safe and the verification process is unable to verify that the code is type safe.");
        }
        else if (ex is IndexOutOfRangeException)
        {
            HLog.Err("an attempt is made to access an element of an array or collection with an index that is outside its bounds.");
        }
        else if (ex is InsufficientExecutionStackException)
        {
            HLog.Err("there is insufficient execution stack available to allow most methods to execute.");
        }
        else if (ex is InvalidCastException)
        {
            HLog.Err("The exception that is thrown for invalid casting or explicit conversion.");
        }
        else if (ex is InvalidProgramException)
        {
            HLog.Err("a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.");
        }
        else if (ex is InvalidTimeZoneException)
        {
            HLog.Err("time zone information is invalid.");
        }
        else if (ex is MissingFieldException)
        {
            HLog.Err("there is an attempt to dynamically access a field that does not exist.");
        }
        else if (ex is MissingMethodException)
        {
            HLog.Err("there is an attempt to dynamically access a method that does not exist.");
        }
        else if (ex is MulticastNotSupportedException)
        {
            HLog.Err("there is an attempt to combine two delegates based on the System.Delegate type instead of the System.MulticastDelegate type. This class cannot be inherited.");
        }
        else if (ex is NotImplementedException)
        {
            HLog.Err("a requested method or operation is not implemented.");
        }
        else if (ex is NullReferenceException)
        {
            HLog.Err("there is an attempt to dereference a null object reference.");
        }
        else if (ex is RankException)
        {
            HLog.Err("an array with the wrong number of dimensions is passed to a method.");
        }
        else if (ex is StackOverflowException)
        {
            HLog.Err("the execution stack overflows because it contains too many nested method calls. This class cannot be inherited.");
        }
        else if (ex is TypeInitializationException)
        {
            HLog.Err("The exception that is thrown as a wrapper around the class initializer. This class cannot be inherited.");
        }
        else if (ex is TypeUnloadedException)
        {
            HLog.Err("there is an attempt to access an unloaded class.");
        }
        else if (ex is System.ComponentModel.LicenseException)
        {
            HLog.Err("a component cannot be granted a license.");
        }
        else if (ex is System.ComponentModel.WarningException)
        {
            HLog.Err("Specifies an exception that is handled as a warning instead of an error.");
        }
        else if (ex is InternalBufferOverflowException)
        {
            HLog.Err("the internal buffer overflows.");
        }
        else if (ex is InvalidDataException)
        {
            HLog.Err("a data stream is in an invalid format.");
        }
        else if (ex is AmbiguousMatchException)
        {
            HLog.Err("binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.");
        }
        else if (ex is System.Resources.MissingManifestResourceException)
        {
            HLog.Err("the main assembly does not contain the resources for the neutral culture, and an appropriate satellite assembly is missing.");
        }
        else if (ex is System.Resources.MissingSatelliteAssemblyException)
        {
            HLog.Err("the satellite assembly for the resources of the default culture is missing.");
        }
        //else
        //{
        //    HLog.Err("System Exception occured");
        //}
    }




    public static void EApplicationException(Exception ex)
    {
        if (ex is InvalidFilterCriteriaException)
        {
            HLog.Err("The exception that is thrown in System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object) when the filter criteria is not valid for the type of filter you are using.");
        }
        else if (ex is TargetException)
        {
            HLog.Err("an attempt is made to invoke an invalid target.");
        }
        else if (ex is TargetInvocationException)
        {
            HLog.Err("methods invoked through reflection. This class cannot be inherited.");
        }
        else if (ex is TargetParameterCountException)
        {
            HLog.Err("the number of parameters for an invocation does not match the number expected. This class cannot be inherited.");
        }
        else if (ex is WaitHandleCannotBeOpenedException)
        {
            HLog.Err("an attempt is made to open a system mutex or semaphore that does not exist.");
        }
        //else
        //{
        //    HLog.Err("Serves as the base class for application-defined exceptions.");
        //}
    }
    public static void ESubArgumentException(Exception ex)
    {
        if (ex is ArgumentNullException)
        {
            HLog.Err("a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.");
        }
        else if (ex is ArgumentOutOfRangeException)
        {
            HLog.Err("the value of an argument is outside the allowable range of values as defined by the invoked method.");
        }
        else if (ex is System.ComponentModel.InvalidAsynchronousStateException)
        {
            HLog.Err("Thrown when a thread on which an operation should execute no longer exists or has no message loop.");
        }
        else if (ex is System.ComponentModel.InvalidEnumArgumentException)
        {
            HLog.Err("using invalid arguments that are enumerators.");
        }
        else if (ex is DuplicateWaitObjectException)
        {
            HLog.Err("an object appears more than once in an array of synchronization objects.");
        }
        else if (ex is System.Globalization.CultureNotFoundException)
        {
            HLog.Err("a method is invoked which attempts to construct a culture that is not available on the machine.");
        }
        else if (ex is DecoderFallbackException)
        {
            HLog.Err("a decoder fallback operation fails. This class cannot be inherited.");
        }
        else if (ex is EncoderFallbackException)
        {
            HLog.Err("an encoder fallback operation fails. This class cannot be inherited.");
        }
        //else
        //{
        //    HLog.Err("one of the arguments provided to a method is not valid.");
        //}
    }
    public static void ESubDataException(Exception ex)
    {
        if (ex is System.Configuration.Provider.ProviderException)
        {
            HLog.Err("a configuration provider error has occurred. This exception class is also used by providers to throw exceptions when internal errors occur within the provider that do not map to other pre-existing exception classes.");
        }
        else if (ex is System.Data.ConstraintException)
        {
            HLog.Err("attempting an action that violates a constraint.");
        }
        else if (ex is System.Data.DBConcurrencyException)
        {
            HLog.Err("the System.Data.Common.DataAdapter during an insert, update, or delete operation if the number of rows affected equals zero.");
        }
        else if (ex is System.Data.DeletedRowInaccessibleException)
        {
            HLog.Err("an action is tried on a System.Data.DataRow that has been deleted.");
        }
        else if (ex is System.Data.DuplicateNameException)
        {
            HLog.Err("a duplicate database object name is encountered during an add operation in a System.Data.DataSet -related object.");
        }
        else if (ex is System.Data.EvaluateException)
        {
            HLog.Err("the System.Data.DataColumn.Expression property of a System.Data.DataColumn cannot be evaluated.");
        }
        else if (ex is System.Data.InRowChangingEventException)
        {
            HLog.Err("you call the System.Data.DataRow.EndEdit method within the System.Data.DataTable.RowChanging event.");
        }
        else if (ex is System.Data.InvalidConstraintException)
        {
            HLog.Err("incorrectly trying to create or access a relation.");
        }
        else if (ex is System.Data.InvalidExpressionException)
        {
            HLog.Err("you try to add a System.Data.DataColumn that contains an invalid System.Data.DataColumn.Expression to a System.Data.DataColumnCollection.");
        }
        else if (ex is System.Data.MissingPrimaryKeyException)
        {
            HLog.Err("you try to access a row in a table that has no primary key.");
        }
        else if (ex is System.Data.NoNullAllowedException)
        {
            HLog.Err("you try to insert a null value into a column where System.Data.DataColumn.AllowDBNull is set to false.");
        }
        else if (ex is System.Data.ReadOnlyException)
        {
            HLog.Err("you try to change the value of a read-only column.");
        }
        else if (ex is System.Data.RowNotInTableException)
        {
            HLog.Err("you try to perform an operation on a System.Data.DataRow that is not in a System.Data.DataTable.");
        }
        else if (ex is System.Data.StrongTypingException)
        {
            HLog.Err("a strongly typed System.Data.DataSet when the user accesses a DBNull value.");
        }
        else if (ex is System.Data.SyntaxErrorException)
        {
            HLog.Err("the System.Data.DataColumn.Expression property of a System.Data.DataColumn contains a syntax error.");
        }
        else if (ex is System.Data.VersionNotFoundException)
        {
            HLog.Err("you try to return a version of a System.Data.DataRow that has been deleted.");
        }
        //else
        //{
        //    HLog.Err("errors are generated using ADO.NET components.");
        //}
    }
    public static void ESubSqlTypeException(Exception ex)
    {
        if (ex is System.Data.SqlTypes.SqlAlreadyFilledException)
        {
            HLog.Err("The System.Data.SqlTypes.SqlAlreadyFilledException class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.");
        }
        else if (ex is System.Data.SqlTypes.SqlNotFilledException)
        {
            HLog.Err("The System.Data.SqlTypes.SqlNotFilledException class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.");
        }
        else if (ex is System.Data.SqlTypes.SqlNullValueException)
        {
            HLog.Err("the Value property of a System.Data.SqlTypes structure is set to null.");
        }
        else if (ex is System.Data.SqlTypes.SqlTruncateException)
        {
            HLog.Err("you set a value into a System.Data.SqlTypes structure would truncate that value.");
        }
        //else
        //{
        //    HLog.Err("The base exception class for the System.Data.SqlTypes.");
        //}
    }
    public static void ESubFormatException(Exception ex)
    {
        if (ex is System.Net.CookieException)
        {
            HLog.Err("an error is made adding a System.Net.Cookie to a System.Net.CookieContainer.");
        }
        else if (ex is CustomAttributeFormatException)
        {
            HLog.Err("the binary format of a custom attribute is invalid.");
        }
        else if (ex is UriFormatException)
        {
            HLog.Err("an invalid Uniform Resource Identifier (URI) is detected.");
        }
        //else
        //{
        //    HLog.Err("the format of an argument is invalid, or when a composite format string is not well formed.");
        //}
    }
    public static void ESubInvalidOperationException(Exception ex)
    {
        if (ex is System.Net.NetworkInformation.PingException)
        {
            HLog.Err("a System.Net.NetworkInformation.Ping.Send or System.Net.NetworkInformation.Ping.SendAsync method calls a method that throws an exception.");
        }
        else if (ex is System.Net.ProtocolViolationException)
        {
            HLog.Err("an error is made while using a network protocol.");
        }
        else if (ex is System.Net.WebException)
        {
            HLog.Err("an error occurs while accessing the network through a pluggable protocol.");
        }
        else if (ex is ObjectDisposedException)
        {
            HLog.Err("an operation is performed on a disposed object.");
        }
        //else
        //{
        //    HLog.Err("a method call is invalid for the object's current state.");
        //}
    }
    public static void ESubIOException(Exception ex)
    {
        if (ex is DirectoryNotFoundException)
        {
            HLog.Err("part of a file or directory cannot be found.");
        }
        else if (ex is DriveNotFoundException)
        {
            HLog.Err("trying to access a drive or share that is not available.");
        }
        else if (ex is EndOfStreamException)
        {
            HLog.Err("reading is attempted past the end of a stream.");
        }
        else if (ex is FileLoadException)
        {
            HLog.Err("a managed assembly is found but cannot be loaded.");
        }
        else if (ex is FileNotFoundException)
        {
            HLog.Err("an attempt to access a file that does not exist on disk fails.");
        }
        else if (ex is PathTooLongException)
        {
            HLog.Err("a path or file name is longer than the system-defined maximum length.");
        }
        //else
        //{
        //    HLog.Err("an I/O error occurs.");
        //}
    }
    public static void ESubMemberAccessException(Exception ex)
    {
        if (ex is FieldAccessException)
        {
            HLog.Err("there is an invalid attempt to access a private or protected field inside a class.");
        }
        else if (ex is MethodAccessException)
        {
            HLog.Err("there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.");
        }
        else if (ex is MissingMemberException)
        {
            HLog.Err("there is an attempt to dynamically access a class member that does not exist.");
        }
        //else
        //{
        //    HLog.Err("an attempt to access a class member fails.");
        //}
    }
    public static void ESubNotSupportedException(Exception ex)
    {
        if (ex is PlatformNotSupportedException)
        {
            HLog.Err("A feature does not run on a particular platform.");
        }
        //else
        //{
        //    HLog.Err("An invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.");
        //}
    }
    public static void ESubOperationCanceledException(Exception ex)
    {
        if (ex is TaskCanceledException)
        {
            HLog.Err("Represents an exception used to communicate task cancellation.");
        }
        //else
        //{
        //    HLog.Err("The exception that is thrown in a thread upon cancellation of an operation that the thread was executing.");
        //}
    }
    public static void ESubOutOfMemoryException(Exception ex)
    {
        if (ex is InsufficientMemoryException)
        {
            HLog.Err("A check for sufficient available memory fails. This class cannot be inherited.");
        }
        //else
        //{
        //    HLog.Err("There is not enough memory to continue the execution of a program.");
        //}
    }
    public static void ESubCryptographicException(Exception ex)
    {
        if (ex is System.Security.Cryptography.CryptographicUnexpectedOperationException)
        {
            HLog.Err("An unexpected operation occurs during a cryptographic operation.");
        }
        //else
        //{
        //    HLog.Err("An error occurs during a cryptographic operation.");
        //}
    }
    public static void ESubTimeoutException(Exception ex)
    {
        if (ex is System.Text.RegularExpressions.RegexMatchTimeoutException)
        {
            HLog.Err("The execution time of a regular expression pattern-matching method exceeds its time-out interval.");
        }
        //else
        //{
        //    HLog.Err("The time allotted for a process or operation has expired.");
        //}
    }
    public static void ESubTransactionException(Exception ex)
    {
        if (ex is System.Transactions.TransactionAbortedException)
        {
            HLog.Err("An operation is attempted on a transaction that has already been rolled back, or an attempt is made to commit the transaction and the transaction aborts.");
        }
        else if (ex is System.Transactions.TransactionInDoubtException)
        {
            HLog.Err("An operation is attempted on a transaction that is in doubt, or an attempt is made to commit the transaction and the transaction becomes InDoubt.");
        }
        else if (ex is System.Transactions.TransactionManagerCommunicationException)
        {
            HLog.Err("A resource manager cannot communicate with the transaction manager.");
        }
        else if (ex is System.Transactions.TransactionPromotionException)
        {
            HLog.Err("A promotion fails.");
        }
        //else
        //{
        //    HLog.Err("You attempt to do work on a transaction that cannot accept new work.");
        //}
    }
    public static void ESubTypeLoadException(Exception ex)
    {
        if (ex is DllNotFoundException)
        {
            HLog.Err("A DLL specified in a DLL import cannot be found.");
        }
        else if (ex is EntryPointNotFoundException)
        {
            HLog.Err("An attempt to load a class fails due to the absence of an entry method.");
        }
        else if (ex is TypeAccessException)
        {
            HLog.Err("A method attempts to use a type that it does not have access to.");
        }
        //else
        //{
        //    HLog.Err("Type-loading failures occur.");
        //}
    }
    public static void ESubUnauthorizedAccessException(Exception ex)
    {
        if (ex is System.Security.AccessControl.PrivilegeNotHeldException)
        {
            HLog.Err("A method in the System.Security.AccessControl namespace attempts to enable a privilege that it does not have.");
        }
        //else
        //{
        //    HLog.Err("The operating system denies access because of an I/O error or a specific type of security error.");
        //}
    }
    public static void ESubXmlSchemaException(Exception ex)
    {
        if (ex is System.Xml.Schema.XmlSchemaInferenceException)
        {
            HLog.Err("Returns information about errors encountered by the System.Xml.Schema.XmlSchemaInference class while inferring a schema from an XML document.");
        }
        else if (ex is System.Xml.Schema.XmlSchemaValidationException)
        {
            HLog.Err("XML Schema Definition Language (XSD) schema validation errors and warnings are encountered in an XML document being validated.");
        }
        //else
        //{
        //    HLog.Err("Returns detailed information about the schema exception.");
        //}
    }
    public static void ESubXsltException(Exception ex)
    {
        if (ex is System.Xml.Xsl.XsltCompileException)
        {
            HLog.Err("The Load method when an error is found in the XSLT style sheet.");
        }
        //else
        //{
        //    HLog.Err("An error occurs while processing an XSLT transformation.");
        //}
    }
    public static void EEventLogException(Exception ex)
    {
        if (ex is System.Diagnostics.Eventing.Reader.EventLogInvalidDataException)
        {
            HLog.Err("An event provider publishes invalid data in an event.");
        }
        else if (ex is System.Diagnostics.Eventing.Reader.EventLogNotFoundException)
        {
            HLog.Err("A requested event log (usually specified by the name of the event log or the path to the event log file) does not exist.");
        }
        else if (ex is System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException)
        {
            HLog.Err("A specified event provider name references a disabled event provider. A disabled event provider cannot publish events.");
        }
        else if (ex is System.Diagnostics.Eventing.Reader.EventLogReadingException)
        {
            HLog.Err("Represents an exception that is thrown when an error occurred while reading, querying, or subscribing to the events in an event log.");
        }
        //else
        //{
        //    HLog.Err("Represents the base class for all the exceptions that are thrown when an error occurs while reading event log related information.");
        //}
    }
    public static void ESmtpException(Exception ex)
    {
        if (ex is System.Net.Mail.SmtpFailedRecipientException)
        {
            if (ex is System.Net.Mail.SmtpFailedRecipientsException)
            {
                HLog.Err("e-mail is sent using an System.Net.Mail.SmtpClient and cannot be delivered to all recipients.");
            }
            else
            {
                HLog.Err("The System.Net.Mail.SmtpClient is not able to complete a System.Net.Mail.SmtpClient.Send or System.Net.Mail.SmtpClient.SendAsync operation to a particular recipient.");
            }
        }
        //else
        //{
        //    HLog.Err("The System.Net.Mail.SmtpClient is not able to complete a System.Net.Mail.SmtpClient.Send or System.Net.Mail.SmtpClient.SendAsync operation.");
        //}
    }
    public static void ESubConfigurationException(Exception ex)
    {
        if (ex is System.Configuration.ConfigurationErrorsException)
        {
            HLog.Err("The current value is not one of the System.Web.Configuration.PagesSection.EnableSessionState values.");
        }
        //else
        //{
        //    HLog.Err("A configuration system error has occurred.");
        //}
    }

    public static void ESubExternalException(Exception ex)
    {
        if (ex is System.ComponentModel.Design.CheckoutException)
        {
            HLog.Err("An attempt to check out a file that is checked into a source code management program is canceled or fails.");
        }
        else if (ex is System.Data.Common.DbException)
        {
            HLog.Err("The base class for all exceptions thrown on behalf of the data source.");
        }
        else if (ex is System.Runtime.InteropServices.SEHException)
        {
            HLog.Err("Represents structured exception handling (SEH) errors.");
        }
        else if (ex is System.Runtime.InteropServices.COMException)
        {
            HLog.Err("An unrecognized HRESULT is returned from a COM method call.");
        }
        else if (ex is System.ComponentModel.Win32Exception)
        {
            EWin32Exception(ex);
        }
        //else
        //{
        //    HLog.Err("The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.");
        //}
    }
    public static void EWin32Exception(Exception ex)
    {
        if (ex is System.Net.HttpListenerException)
        {
            HLog.Err("An error occurs processing an HTTP request.");
        }
        else if (ex is System.Net.NetworkInformation.NetworkInformationException)
        {
            HLog.Err("An error occurs while retrieving network information.");
        }
        else if (ex is System.Net.Sockets.SocketException)
        {
            HLog.Err("A socket error occurs.");
        }
        else if (ex is System.Net.WebSockets.WebSocketException)
        {
            HLog.Err("Represents an exception that occurred when performing an operation on a WebSocket connection.");
        }
        else if (ex is ReflectionTypeLoadException)
        {
            HLog.Err("The System.Reflection.Module.GetTypes method if any of the classes in a module cannot be loaded. This class cannot be inherited.");
        }
        else
        {
            HLog.Err("Throws an exception for a Win32 error code.");
        }
    }

    public static void ESubAuthenticationException(Exception ex)
    {
        if (ex is System.Security.Authentication.InvalidCredentialException)
        {
            HLog.Err("Authentication fails for an authentication stream and cannot be retried.");
        }
        else
        {
            HLog.Err("Authentication fails for an authentication stream.");
        }
    }



    public static string ToFullDisplayString(this Exception ex)
    {
        StringBuilder displayText = new ();
        WriteExceptionDetail(displayText, ex);
        foreach (Exception inner in ex.GetNestedExceptionList())
        {
            displayText.AppendFormat("****内部例外 始点 ****｛0｝", Environment.NewLine);
            WriteExceptionDetail(displayText, inner);
            displayText.AppendFormat("****内部例外 終点 ****{0}{0}", Environment.NewLine);
        }
        return displayText.ToString();
    }
    public static void WriteExceptionDetail(StringBuilder builder, Exception ex)
    {
        builder.AppendFormat("Message: {0}{1}", ex.Message, Environment.NewLine);
        builder.AppendFormat("Type: {0}{1}", ex.GetType(), Environment.NewLine);
        builder.AppendFormat("HelpLink: {0}{1}", ex.HelpLink, Environment.NewLine);
        builder.AppendFormat("Source: {0}{1}", ex.Source, Environment.NewLine);
        builder.AppendFormat("TargetSite: {0}{1}", ex.TargetSite, Environment.NewLine);
        builder.AppendFormat("Data: {0}", Environment.NewLine);
        foreach (DictionaryEntry de in ex.Data)
        {
            builder.AppendFormat("\t{0} : {1}{2}", de.Key, de.Value, Environment.NewLine);
        }
        builder.AppendFormat("StackTrace: {0}{1}", ex.StackTrace, Environment.NewLine);
    }

    public static IEnumerable<Exception> GetNestedExceptionList(this Exception exception)
    {
        Exception current = exception;
        do
        {
            current = current.InnerException;
            if (current != null)
            {
                yield return current;
            }
        }
        while (current != null);
    }

    public static string GetStackTraceIndo()
    {
        StringBuilder result = new ();
        StackTrace st = new (true);
        StackFrame[] frames = st.GetFrames();
        //最初のStackFrameはGetstackTraceInfo自身なので除外する
        for (int i = 1; i < frames.Length; i++)
        {
            result.Append(frames[i].ToString());
        }
        return result.ToString();
    }
    public static void LastChanceHandler(object sender, UnhandledExceptionEventArgs args)
    {
        try
        {
            Exception e = (Exception)args.ExceptionObject;

            Console.WriteLine("未処理の例外==" + e.ToString());
            if (args.IsTerminating)
            {
                Console.WriteLine("アプリケーションを終了しています");
            }
            else
            {
                Console.WriteLine("アプリケーションは終了されません");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("未処理冷害に対するハンドル中で発生した未処理例外==" + e.ToString());
        }
        finally
        {

        }
    }

    //ThreadExceptionクラス .NET8 非対応
    //https://learn.microsoft.com/ja-jp/dotnet/api/system.windows.forms.application.threadexception?view=windowsdesktop-9.0

    //static void Main()
    //{
    //    //メインのUIスレッドで発生したすべての例外に対する
    //    //イベントハンドラを追加する
    //    Application.ThreadException +=
    //    new System.Threading.ThreadExceptionEventHandler(OnThreadException);
    //    //アプリケーションドメイン内のメインのUIスレッドを除くすべてのスレッドに
    //    // 対してイベントハンドラを追加する
    //    AppDomain.CurrentDomain.UnhandledException +=
    //        new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
    //    Application.EnableVisualStyles();
    //    Application.SetCompatibleTextRenderingDefault(false);
    //    Application.Run(new Form1());

    //}

    // UI スレッド以外の例外を処理する
    //static void CurrentDomain_UnhandledException(object sender,UnhandledExceptionEventArgs e)
    //{
    //    //単に例外情報を表示する
    //    MessageBox.Show("CurrentDomain UnhandledException: "
    //        + e.ExceptionObject.ToString());
    //}

    //static async void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    //{
    //    //単に例外情報を表示する
    //    MessageBox.Show("OnThreadException: " + e.Exception.ToString());
    //    display
    //}




    public static void UnSupportedExceptionVersion(Exception ex)
    {
        ////else if (ex is System.UriTemplateMatchException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an error when matching a System.Uri to a System.UriTemplateTable.");
        ////}
        ////else if (ex is System.Activities.InvalidWorkflowException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Handles exceptions that occur when a workflow is not valid.");
        ////}
        ////else if (ex is System.Activities.ValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a workflow or activity is in an invalid state.");
        ////}
        ////else if (ex is System.Activities.VersionMismatchException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Specifies information about a version mismatch exception.");
        ////}
        ////else if (ex is System.Activities.WorkflowApplicationAbortedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an operation on a workflow instance is not valid because the instance has been aborted.");
        ////}
        ////else if (ex is System.Activities.WorkflowApplicationCompletedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an operation on a workflow instance is not valid because the instance has completed.");
        ////}
        ////else if (ex is System.Activities.WorkflowApplicationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Provides the base class for handling an error that occurs during the execution of a workflow application.");
        ////}
        ////else if (ex is System.Activities.WorkflowApplicationTerminatedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("An exception that is thrown when an operation on a workflow instance is not valid because the instance has been terminated.");
        ////}
        ////else if (ex is System.Activities.WorkflowApplicationUnloadedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("An exception that is thrown when an operation on a workflow instance is invalid because the instance has been unloaded.");
        ////}
        ////else if (ex is System.Activities.DynamicUpdate.InstanceUpdateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an instance update exception.");
        ////}
        ////else if (ex is System.Activities.ExpressionParser.SourceExpressionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a text-based expression cannot be converted into an executable form.");
        ////}
        ////else if (ex is System.Activities.Expressions.LambdaSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a XAML serialization attempt is made on a System.Activities.Expressions.LambdaValue`1 or System.Activities.Expressions.LambdaReference`1.");
        ////}
        ////else if (ex is System.Activities.Presentation.Metadata.AttributeTableValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Provides the functionality to throw an exception from the System.Activities.Presentation.Metadata.AttributeTableBuilder.ValidateTable method if the metadata provided in the table does not match properties, methods, and events on real types.");
        ////}
        ////else if (ex is System.Activities.Statements.WorkflowTerminatedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an operation is invoked on a terminated System.Activities.WorkflowApplication.");
        ////}
        ////else if (ex is System.AddIn.Hosting.AddInSegmentDirectoryNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a segment directory is missing from the pipeline directory structure.");
        ////}
        ////else if (ex is System.AddIn.Hosting.InvalidPipelineStoreException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a directory is not found and the user does not have permission to access the pipeline root path or an add-in path.");
        ////}
        ////else if (ex is System.ComponentModel.Composition.ChangeRejectedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("An exception that indicates whether a part has been rejected during composition.");
        ////}
        ////else if (ex is System.ComponentModel.Composition.CompositionContractMismatchException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the underlying exported value or metadata of a System.Lazy`1 or System.Lazy`2 object cannot be cast to T or TMetadataView, respectively.");
        ////}
        ////else if (ex is System.ComponentModel.Composition.CompositionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("one or more errors occur during composition in a System.ComponentModel.Composition.Hosting.CompositionContainer object.");
        ////}
        ////else if (ex is System.ComponentModel.Composition.ImportCardinalityMismatchException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the cardinality of an import is not compatible with the cardinality of the matching exports.");
        ////}
        ////else if (ex is System.ComponentModel.Composition.Primitives.ComposablePartException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs when calling methods on a System.ComponentModel.Composition.Primitives.ComposablePart object.");
        ////}

        ////else if (ex is System.ComponentModel.Design.Serialization.CodeDomSerializerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("line number information is available for a serialization error.");
        ////}

        ////else if (ex is System.Configuration.Install.InstallException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs during the commit, rollback, or uninstall phase of an installation.");
        ////}

        ////else if (ex is System.Data.EntityCommandCompilationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents errors that occur during command compilation; when a command tree could not be produced to represent the command text.");
        ////}
        ////else if (ex is System.Data.EntityCommandExecutionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents errors that occur when the underlying storage provider could not execute the specified command. This exception usually wraps a provider-specific exception.");
        ////}
        ////else if (ex is System.Data.EntityException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents Entity Framework-related errors that occur in the EntityClient namespace. The EntityException is the base class for all Entity Framework exceptions thrown by the EntityClient.");
        ////}
        ////else if (ex is System.Data.EntitySqlException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents errors that occur when parsing Entity SQL command text. This exception is thrown when syntactic or semantic rules are violated.");
        ////}

        ////else if (ex is System.Data.InvalidCommandTreeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that is thrown to indicate that a command tree is invalid. This exception is currently not thrown anywhere in the Entity Framework.");
        ////}

        ////else if (ex is System.Data.MappingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("mapping related service requests fail.");
        ////}
        ////else if (ex is System.Data.MetadataException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("metadata related service requests fails.");
        ////}

        ////else if (ex is System.Data.ObjectNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an object is not present.");
        ////}
        ////else if (ex is System.Data.OperationAbortedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when an ongoing operation is aborted by the user.");
        ////}
        ////else if (ex is System.Data.OptimisticConcurrencyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an optimistic concurrency violation occurs.");
        ////}
        ////else if (ex is System.Data.PropertyConstraintException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Property constraint exception class. Note that this class has state - so if you change even its internals, it can be a breaking change.");
        ////}
        ////else if (ex is System.Data.ProviderIncompatibleException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the underlying data provider is incompatible with the Entity Framework.");
        ////}

        ////else if (ex is System.Data.TypedDataSetGeneratorException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a name conflict occurs while generating a strongly typed System.Data.DataSet.");
        ////}
        ////else if (ex is System.Data.UpdateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("modifications to object instances cannot be persisted to the data source.");
        ////}

        ////else if (ex is System.Data.Design.TypedDataSetGeneratorException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a name conflict occurs while a strongly typed System.Data.DataSet is being generated.");
        ////}
        ////else if (ex is System.Data.Linq.ChangeConflictException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Thrown when an update fails because database values have been updated since the client last read them.");
        ////}
        ////else if (ex is System.Data.Linq.DuplicateKeyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Thrown when an attempt is made to add an object to the identity cache by using a key that is already being used.");
        ////}
        ////else if (ex is System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents errors that occur when an attempt is made to change a foreign key when the entity is already loaded.");
        ////}
        ////else if (ex is System.Data.Odbc.OdbcException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that is generated when a warning or error is returned by an ODBC data source. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Data.OleDb.OleDbException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the underlying provider returns a warning or error for an OLE DB data source. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Data.OracleClient.OracleException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that is generated when a warning or error is returned by an Oracle database or the .NET Framework Data Provider for Oracle. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Data.Services.DataServiceException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an instance of the System.Data.Services.DataServiceException class with a specified message that describes the error.");
        ////}
        ////else if (ex is System.Data.Services.Client.DataServiceClientException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents errors that occur during execution of WCF Data Services client applications.");
        ////}
        ////else if (ex is System.Data.Services.Client.DataServiceQueryException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Exception that indicates an error occurred loading the property value from the data service.");
        ////}
        ////else if (ex is System.Data.Services.Client.DataServiceRequestException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents the error thrown if the data service returns a response code less than 200 or greater than 299, or the top-level element in the response is &lt;error&gt;. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Data.SqlClient.SqlException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("SQL Server returns a warning or error. This class cannot be inherited.");
        ////}

        ////else if (ex is System.Deployment.Application.CompatibleFrameworkMissingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when a version of the .NET Framework that is compatible with this application cannot be found.");
        ////}
        ////else if (ex is System.Deployment.Application.DependentPlatformMissingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the platform dependency is not found during activation of the ClickOnce deployment and the deployment will not run.");
        ////}
        ////else if (ex is System.Deployment.Application.DeploymentDownloadException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that there was an error downloading either the ClickOnce manifests or the deployment's files to the client computer.");
        ////}
        ////else if (ex is System.Deployment.Application.DeploymentException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Defines a base class for all deployment-related exceptions.");
        ////}
        ////else if (ex is System.Deployment.Application.InvalidDeploymentException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that ClickOnce could not read either the deployment or application manifests.");
        ////}
        ////else if (ex is System.Deployment.Application.SupportedRuntimeMissingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when a runtime version that is compatible with this application cannot be found.");
        ////}
        ////else if (ex is System.Deployment.Application.TrustNotGrantedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that the application does not have the appropriate level of trust to run on the local computer.");
        ////}
        ////else if (ex is System.DirectoryServices.DirectoryServicesCOMException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Contains extended error information about an error that occurred when the System.DirectoryServices.DirectoryEntry.Invoke(System.String,System.Object[]) method is called.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.MultipleMatchesException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown by methods that expect to match a single principal object when there are multiple matches to the search query.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.NoMatchingPrincipalException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when no matching principal object could be found with the specified parameters.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.PasswordException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when a password does not meet complexity requirements.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.PrincipalException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The base class of exceptions thrown by System.DirectoryServices.AccountManagement objects.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.PrincipalExistsException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Thrown by System.DirectoryServices.AccountManagement.PrincipalCollection.Add when an attempt is made to insert a principal that already exists in the collection, or by System.DirectoryServices.AccountManagement.Principal.Save when an attempt is made to save a new principal that already exists in the store.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.PrincipalOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Thrown when ADSI returns an error during an operation to update the store.");
        ////}
        ////else if (ex is System.DirectoryServices.AccountManagement.PrincipalServerDownException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown when the API is unable to connect to the server.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException class exception is thrown when an Active Directory Domain Services object is created and that object already exists in the underlying directory store.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException class exception is thrown when a requested object is not found in the underlying directory store.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException class exception is thrown when an underlying directory operation fails.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException class exception is thrown when a server is unavailable to respond to a service request.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException class exception is thrown when a trust collision occurs during a trust relationship management request.");
        ////}
        ////else if (ex is System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException exception is thrown when the request to synchronize from all servers fails.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.BerConversionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.BerConversionException class is an exception thrown when converting data using a System.DirectoryServices.Protocols.BerConverter object.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.DirectoryException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.DirectoryException class is an abstract class used as the base class for all System.DirectoryServices.Protocols exceptions.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.DirectoryOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.DirectoryOperationException class is an exception thrown by the System.DirectoryServices.Protocols.LdapConnection.SendRequest(System.DirectoryServices.Protocols.DirectoryRequest) method to indicate that the server returned a System.DirectoryServices.Protocols.DirectoryResponse object with an error.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.DsmlInvalidDocumentException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.DsmlInvalidDocumentException class is an exception that occurs when a DSML Request or Response document is not well-formed XML or cannot be validated with DSMLv2 schema.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.ErrorResponseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.ErrorResponseException class is an exception that occurs when the server returns an &amp;lt;errorResponse&amp;gt;.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.LdapException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.LdapException class is an exception that occurs when LDAP returns an error code not included in System.DirectoryServices.Protocols.ResultCode.");
        ////}
        ////else if (ex is System.DirectoryServices.Protocols.TlsOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The System.DirectoryServices.Protocols.TlsOperationException class is an exception that occurs in the System.DirectoryServices.Protocols.LdapSessionOptions.StartTransportLayerSecurity(System.DirectoryServices.Protocols.DirectoryControlCollection) method if the request fails.");
        ////}
        ////else if (ex is System.Drawing.Printing.InvalidPrinterException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("you try to access a printer using printer settings that are not valid.");
        ////}
        ////else if (ex is System.EnterpriseServices.RegistrationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a registration error is detected.");
        ////}
        ////else if (ex is System.EnterpriseServices.ServicedComponentException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error is detected in a serviced component.");
        ////}
        ////else if (ex is System.IdentityModel.AsynchronousOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs during an asynchronous operation.");
        ////}
        ////else if (ex is System.IdentityModel.BadRequestException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a token request (RST) is not understood by the security token service (STS).");
        ////}
        ////else if (ex is System.IdentityModel.LimitExceededException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a configured limit or quota is exceeded.");
        ////}
        ////else if (ex is System.IdentityModel.RequestException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The base class for exceptions thrown on request failures.");
        ////}
        ////else if (ex is System.IdentityModel.RequestFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the specified request (RST) failed due to an external reason that cannot be specifically determined.");
        ////}
        ////else if (ex is System.IdentityModel.SecurityMessageSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while serializing a security message.");
        ////}
        ////else if (ex is System.IdentityModel.SignatureVerificationFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while processing a signature");
        ////}
        ////else if (ex is System.IdentityModel.UnsupportedTokenTypeBadRequestException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the specified token request (RST) is not understood because of an unknown token type.");
        ////}
        ////else if (ex is System.IdentityModel.Metadata.MetadataSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while serializing or deserializing SAML metadata.");
        ////}
        ////else if (ex is System.IdentityModel.Protocols.WSTrust.InvalidRequestException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the request (RST) is invalid or malformed.");
        ////}
        ////else if (ex is System.IdentityModel.Protocols.WSTrust.WSTrustSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while serializing or deserializing a WS-Trust message.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.CardSpaceException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("one or more exceptions have occurred at the CardSpace service level. The cause of the error will be logged in the event log.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.IdentityValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Exception class to indicate that the recipient certificate was not valid.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.PolicyValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Exception class to indicate that the policy supplied by the recipient could not be validated.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.ServiceBusyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Exception class to indicate that the CardSpace service is busy processing other requests.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.ServiceNotStartedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("CardSpace has not been started on the user's computer.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.StsCommunicationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("there is a problem communicating with the security token service.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.UnsupportedPolicyOptionsException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that a policy was provided to the system that included options that were unsupported.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.UntrustedRecipientException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the user decides not to trust the entity that is requesting a token after reviewing the information from their certificate.");
        ////}
        ////else if (ex is System.IdentityModel.Selectors.UserCancellationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the user cancels an operation during the System.IdentityModel.Selectors.CardSpaceSelector.GetToken(System.IdentityModel.Selectors.CardSpacePolicyElement[],System.IdentityModel.Selectors.SecurityTokenSerializer) call.");
        ////}
        ////else if (ex is System.IdentityModel.Services.AsynchronousOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs during an asynchronous operation.");
        ////}
        ////else if (ex is System.IdentityModel.Services.FederatedAuthenticationSessionEndingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that the sign-in session should being terminated, and the current request is unauthenticated.");
        ////}
        ////else if (ex is System.IdentityModel.Services.FederatedSessionExpiredException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a session has expired.");
        ////}
        ////else if (ex is System.IdentityModel.Services.FederationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Base class for exceptions raised in WS-Federation support.");
        ////}
        ////else if (ex is System.IdentityModel.Services.WSFederationMessageException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while serializing or deserializing a WS-Federation message.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.AudienceUriValidationFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an incoming security token fails Audience URI validation.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.EncryptedTokenDecryptionFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs while processing an encrypted security token.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.SecurityTokenException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a problem occurs while processing a security token.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.SecurityTokenExpiredException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a security token that has an expiration time in the past is received.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.SecurityTokenNotYetValidException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a security token that has an effective time in the future is received.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.SecurityTokenReplayDetectedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a security token that has been replayed is received.");
        ////}
        ////else if (ex is System.IdentityModel.Tokens.SecurityTokenValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a received security token is invalid.");
        ////}

        ////else if (ex is System.IO.FileFormatException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an input file or a data stream that is supposed to conform to a certain file format specification is malformed.");
        ////}
        ////else if (ex is System.IO.PipeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Thrown when an error occurs within a named pipe.");
        ////}
        ////else if (ex is System.IO.Log.ReservationNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a specific space reservation in a System.IO.Log.LogRecordSequence is not found.");
        ////}
        ////else if (ex is System.IO.Log.SequenceFullException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a sequence of log records is full.");
        ////}
        ////else if (ex is System.Management.ManagementException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents management exceptions.");
        ////}
        ////else if (ex is System.Management.Automation.ActionPreferenceStopException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ApplicationFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ArgumentTransformationMetadataException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.BreakException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.CmdletInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.CmdletProviderInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.CommandNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ContinueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.DriveNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ExitException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ExtendedTypeSystemException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.FlowControlException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.GetValueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.GetValueInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.HaltCommandException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.IncompleteParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.InvalidJobStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.InvalidPowerShellStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ItemNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.JobFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.LoopFlowException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.MetadataException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.MethodException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.MethodInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ParameterBindingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ParentContainsErrorRecordException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ParsingMetadataException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PipelineClosedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PipelineDepthException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PipelineStoppedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PropertyNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ProviderInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ProviderNameAmbiguousException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ProviderNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSArgumentException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSArgumentNullException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSArgumentOutOfRangeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSInvalidCastException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSInvalidOperationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSNotImplementedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSNotSupportedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSObjectDisposedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.PSSecurityException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.RedirectedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.RemoteException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.RuntimeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ScriptBlockToPowerShellNotSupportedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ScriptCallDepthException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ScriptRequiresException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.SessionStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.SessionStateOverflowException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.SessionStateUnauthorizedAccessException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.SetValueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.SetValueInvocationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.TerminateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.ValidationMetadataException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.WildcardPatternException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Host.HostException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Host.PromptingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Remoting.PSDirectException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Remoting.PSRemotingDataStructureException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Remoting.PSRemotingTransportException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Remoting.PSRemotingTransportRedirectException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.FormatTableLoadException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.InvalidPipelineStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.InvalidRunspacePoolStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.InvalidRunspaceStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.PSConsoleLoadException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.PSSnapInException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.RunspaceConfigurationAttributeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.RunspaceConfigurationTypeException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.RunspaceOpenModuleLoadException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Automation.Runspaces.TypeTableLoadException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("");
        ////}
        ////else if (ex is System.Management.Instrumentation.InstanceNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception thrown to indicate that no instances are returned by a provider.");
        ////}
        ////else if (ex is System.Management.Instrumentation.InstrumentationBaseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents the base provider-related exception.");
        ////}
        ////else if (ex is System.Management.Instrumentation.InstrumentationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a provider-related exception.");
        ////}
        ////else if (ex is System.Management.Instrumentation.WmiProviderInstallationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an exception to throw when WMI provider installation fails.");
        ////}
        ////else if (ex is System.Messaging.MessageQueueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a Microsoft Message Queuing internal error occurs.");
        ////}
        ////else if (ex is System.Net.PeerToPeer.PeerToPeerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents the exceptions that are thrown when an error is raised by the Peer-to-Peer Infrastructure.");
        ////}

        ////else if (ex is System.Printing.PrintCommitAttributesException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error condition prevents some attributes from being committed by a System.Printing.PrintSystemObject to the actual computer, printer, or device that the object represents.");
        ////}
        ////else if (ex is System.Printing.PrintingCanceledException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that occurs when code attempts to access a canceled print job.");
        ////}
        ////else if (ex is System.Printing.PrintingNotSupportedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a printing operation is not supported.");
        ////}
        ////else if (ex is System.Printing.PrintJobException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that occurs when the print job does not run correctly.");
        ////}
        ////else if (ex is System.Printing.PrintQueueException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error condition prevents the accessing or creation of a System.Printing.PrintQueue.");
        ////}
        ////else if (ex is System.Printing.PrintServerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that occurs when an error condition prevents the accessing or creation of a System.Printing.PrintServer.");
        ////}
        ////else if (ex is System.Printing.PrintSystemException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that occurs when an error condition prevents accessing or creating a System.Printing.PrintSystemObject.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceCollisionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects an instance to be in an uninitialized state but the instance is not in that state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceCompleteException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects to find an instance in the initialized state, but finds the instance is in the completed state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceHandleConflictException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it tries to acquire write access to an instance by binding an instance handle to an instance lock, when an instance handle with write access to that instance already exists.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceKeyCollisionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects to find an instance key in the unassociated state, but finds the key in a different state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceKeyCompleteException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects to find an instance key in the associated state but finds the key in the completed state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceKeyNotReadyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects to find an instance key in the associated state, but finds the key in the unassociated state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceLockedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it is unable to acquire a lock on an instance because the instance is already locked by another owner.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceLockLostException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it cannot perform the command because the lock on the instance does not match the lock associated with the instance handle against which the command was executed. Either the owner or the version does not match.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceNotReadyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when it expects to find an instance in an initialized state, but finds the instance in an uninitialized state.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstanceOwnerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when the instance owner bound to the instance handle has become invalid.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstancePersistenceCommandException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("A persistence provider throws this exception when an error occurs while processing a persistence command. The persistence provider may also free the instance handle against which the command was executed if the error would extend to future uses of the instance handle.");
        ////}
        ////else if (ex is System.Runtime.DurableInstancing.InstancePersistenceException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Base class for all the persistence related exception classes. The System.Runtime.DurableInstancing.InstanceOwnerException and the System.Runtime.DurableInstancing.InstancePersistenceCommandException are derived classes of this class.");
        ////}
        ////        else if (ex is System.Runtime.InteropServices.Exception)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////            HLog.Err("Exposes the public members of the System.Exception class to unmanaged code.");
        ////        }
        ////else if (ex is System.Runtime.Remoting.RemotingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("something has gone wrong during remoting.");
        ////}
        ////else if (ex is System.Runtime.Remoting.RemotingTimeoutException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the server or the client cannot be reached for a previously specified period of time.");
        ////}
        ////else if (ex is System.Runtime.Remoting.ServerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that is thrown to communicate errors to the client when the client connects to non-.NET Framework applications that cannot throw exceptions.");
        ////}
        ////else if (ex is System.Runtime.Remoting.MetadataServices.SUDSGeneratorException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs during the generation of Web Services Description Language (WSDL).");
        ////}
        ////else if (ex is System.Runtime.Remoting.MetadataServices.SUDSParserException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an error occurs during parsing of the Web Services Description Language (WSDL).");
        ////}

        ////else if (ex is System.Security.HostProtectionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a denied host resource is detected.");
        ////}

        ////else if (ex is System.Security.XmlSyntaxException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("there is a syntax error in XML parsing. This class cannot be inherited.");
        ////}




        ////else if (ex is System.Security.Policy.PolicyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("policy forbids code to run.");
        ////}

        ////else if (ex is System.Security.RightsManagement.RightsManagementException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an error condition when a rights management operation cannot complete successfully.");
        ////}
        ////else if (ex is System.ServiceModel.ActionNotSupportedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is typically thrown on the client when the action related to the operation invoked does not match any action of operations in the server.");
        ////}
        ////else if (ex is System.ServiceModel.AddressAccessDeniedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("access to the address is denied.");
        ////}
        ////else if (ex is System.ServiceModel.AddressAlreadyInUseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an address is unavailable because it is already in use.");
        ////}
        ////else if (ex is System.ServiceModel.ChannelTerminatedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is typically thrown on the client when a channel is terminated due to server closing the associated connection.");
        ////}
        ////else if (ex is System.ServiceModel.CommunicationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a communication error in either the service or client application.");
        ////}
        ////else if (ex is System.ServiceModel.CommunicationObjectAbortedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the call is to an System.ServiceModel.ICommunicationObject object that has aborted.");
        ////}
        ////else if (ex is System.ServiceModel.CommunicationObjectFaultedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a call is made to a communication object that has faulted.");
        ////}
        ////else if (ex is System.ServiceModel.EndpointNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a remote endpoint could not be found or reached.");
        ////}
        ////else if (ex is System.ServiceModel.FaultException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a SOAP fault.");
        ////}
        ////else if (ex is System.ServiceModel.InvalidMessageContractException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a message contract that is not valid.");
        ////}
        ////else if (ex is System.ServiceModel.MessageHeaderException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the expectations regarding headers of a SOAP message are not satisfied when the message is processed.");
        ////}
        ////else if (ex is System.ServiceModel.MsmqException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Encapsulates errors returned by Message Queuing (MSMQ). This exception is thrown by the Message Queuing transport and the Message Queuing integration channel.");
        ////}
        ////else if (ex is System.ServiceModel.MsmqPoisonMessageException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Encapsulates the channel detects that the message is a poison message.");
        ////}
        ////else if (ex is System.ServiceModel.PoisonMessageException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("An exception that is thrown when the message is deemed poison. A message is poisoned if it fails repeated attempts to deliver the message.");
        ////}
        ////else if (ex is System.ServiceModel.ProtocolException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception seen on the client that is thrown when communication with the remote party is impossible due to mismatched data transfer protocols.");
        ////}
        ////else if (ex is System.ServiceModel.QuotaExceededException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a message quota has been exceeded.");
        ////}
        ////else if (ex is System.ServiceModel.ServerTooBusyException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a server is too busy to accept a message.");
        ////}
        ////else if (ex is System.ServiceModel.ServiceActivationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a service fails to activate.");
        ////}
        ////else if (ex is System.ServiceModel.Channels.InvalidChannelBindingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the binding specified is not consistent with the contract requirements for the service.");
        ////}
        ////else if (ex is System.ServiceModel.Channels.RedirectionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an error that occurs in redirection processing.");
        ////}
        ////else if (ex is System.ServiceModel.Channels.RetryException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a retry exception that can be used by a messaging host such as System.ServiceModel.Activities,WorkflowServiceHost to communicate any cancellation of an attempted operation to the client.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.FilterInvalidBodyAccessException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a filter or filter table attempts to access the body of an unbuffered message.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.InvalidBodyAccessException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("An abstract base class for the exceptions that are thrown if an attempt is made to access the body of a message when it is not allowed.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.MessageFilterException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The base class for the exceptions that are thrown when the quota of nodes inspected by a filter is exceeded.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.MultipleFilterMatchesException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("multiple filters match, but only one was expected.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.NavigatorInvalidBodyAccessException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("an System.Xml.XPath.XPathNavigator is directed to examine the body of an unbuffered message.");
        ////}
        ////else if (ex is System.ServiceModel.Dispatcher.XPathNavigatorException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("the quota of nodes allowed to be inspected by an XPathNavigator is exceeded.");
        ////}
        ////else if (ex is System.ServiceModel.Persistence.InstanceLockException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is intended for use by classes that inherit from System.ServiceModel.Persistence.LockingPersistenceProvider when the operation cannot be performed because of the state of the instance lock.");
        ////}
        ////else if (ex is System.ServiceModel.Persistence.InstanceNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("The exception that is thrown under the following circumstances: an operation is performed on a durable service instance that has been marked for completion, or a persistence provider created by a System.ServiceModel.Persistence.SqlPersistenceProviderFactory attempts to lock, unlock, or otherwise process state data that is not found in the database.");
        ////}
        ////else if (ex is System.ServiceModel.Persistence.PersistenceException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("This exception is thrown by a System.ServiceModel.Persistence.SqlPersistenceProviderFactory when general connectivity errors are encountered.");
        ////}
        ////else if (ex is System.ServiceModel.Security.ExpiredSecurityTokenException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Exception thrown when a CardSpace security token expires.");
        ////}
        ////else if (ex is System.ServiceModel.Security.MessageSecurityException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents an exception that occurred when there is something wrong with the security applied on a message.");
        ////}
        ////else if (ex is System.ServiceModel.Security.SecurityAccessDeniedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents the security exception that is thrown when a security authorization request fails.");
        ////}
        ////else if (ex is System.ServiceModel.Security.SecurityNegotiationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Indicates that an error occurred while negotiating the security context for a message.");
        ////}
        ////else if (ex is System.ServiceModel.Web.WebFaultException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("Represents a fault that can have an associated HTTP status code.");
        ////}
        ////else if (ex is System.ServiceProcess.TimeoutException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    HLog.Err("a specified timeout has expired.");
        ////}
        ////else if (ex is System.Web.HttpCompileException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a compiler error occurs.");
        ////}
        ////else if (ex is System.Web.HttpException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Describes an exception that occurred during the processing of HTTP requests.");
        ////}
        ////else if (ex is System.Web.HttpParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a parse error occurs.");
        ////}
        ////else if (ex is System.Web.HttpRequestValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a potentially malicious input string is received from the client as part of the request data. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Web.HttpUnhandledException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a generic exception occurs.");
        ////}
        ////else if (ex is System.Web.Caching.DatabaseNotEnabledForNotificationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a SQL Server database is not enabled to support dependencies associated with the System.Web.Caching.SqlCacheDependency class. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Web.Caching.TableNotEnabledForNotificationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a System.Web.Caching.SqlCacheDependency class is used against a database table that is not enabled for change notifications.");
        ////}
        ////else if (ex is System.Web.DynamicData.IDynamicValidatorException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents an interface implemented by Dynamic Data Exception classes.");
        ////}
        ////else if (ex is System.Web.Management.SqlExecutionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Defines a class for SQL execution exceptions in the System.Web.Management namespace.");
        ////}
        ////else if (ex is System.Web.Query.Dynamic.ParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents errors that occur when a System.Web.UI.WebControls.LinqDataSource control parses values to create a query.");
        ////}
        ////else if (ex is System.Web.Security.MembershipCreateUserException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a user is not successfully created by a membership provider.");
        ////}
        ////else if (ex is System.Web.Security.MembershipPasswordException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a password cannot be retrieved from the password store.");
        ////}
        ////else if (ex is System.Web.Services.Protocols.SoapException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("an XML Web service method is called over SOAP and an exception occurs.");
        ////}
        ////else if (ex is System.Web.Services.Protocols.SoapHeaderException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("The SOAP representation of a server error.");
        ////}
        ////else if (ex is System.Web.UI.ViewStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the view state cannot be loaded or validated. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Web.UI.WebControls.EntityDataSourceValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents errors that occur when validating properties of a dynamic data source.");
        ////}
        ////else if (ex is System.Web.UI.WebControls.LinqDataSourceValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Describes an exception that occurred during validation of new or modified values before values are inserted, updated, or deleted by a System.Web.UI.WebControls.LinqDataSource control.");
        ////}
        ////else if (ex is System.Windows.ResourceReferenceKeyNotFoundException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a resource reference key cannot be found during parsing or serialization of markup extension resources.");
        ////}
        ////else if (ex is System.Windows.Automation.ElementNotAvailableException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Contains information about the exception that is raised when an attempt is made to access an UI Automation element corresponding to a part of the user interface that is no longer available.");
        ////}
        ////else if (ex is System.Windows.Automation.ElementNotEnabledException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Contains information about the exception that is raised when an attempt is made to manipulate a control that is not enabled.");
        ////}
        ////else if (ex is System.Windows.Automation.NoClickablePointException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Contains information about the exception that is raised when System.Windows.Automation.AutomationElement.GetClickablePoint is called on a UI Automation element that has no clickable point.");
        ////}
        ////else if (ex is System.Windows.Automation.ProxyAssemblyNotLoadedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Contains information about an exception that is raised when there is a problem loading an assembly that contains client-side providers");
        ////}
        ////else if (ex is System.Windows.Controls.PrintDialogException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("an error condition occurs during the opening, accessing, or using of a PrintDialog.");
        ////}
        ////else if (ex is System.Windows.Data.ValueUnavailableException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the System.Windows.Data.BindingGroup.GetValue(System.Object,System.String) method when the value is not available.");
        ////}
        ////else if (ex is System.Windows.Forms.AxHost.InvalidActiveXStateException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the ActiveX control is referenced while in an invalid state.");
        ////}
        ////else if (ex is System.Windows.Markup.XamlParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents the exception class for parser-specific exceptions from a WPF XAML parser. This exception is used in XAML API or WPF XAML parser operations from .NET Frameworká3.0 and .NET Frameworká3.5, or for specific use of the WPF XAML parser by calling System.Windows.Markup.XamlReader API.");
        ////}
        ////else if (ex is System.Windows.Media.InvalidWmpVersionException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the installed Microsoft Windows Media Player version is not supported.");
        ////}
        ////else if (ex is System.Windows.Media.Animation.AnimationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("an error occurs while animating a property.");
        ////}
        ////else if (ex is System.Windows.Xps.XpsException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Serves as the base class for exceptions that are thrown by the XML Paper Specification (XPS) packaging and serialization APIs.");
        ////}
        ////else if (ex is System.Windows.Xps.XpsPackagingException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("reading, writing to, registering, or accessing in some other way an System.Windows.Xps.Packaging.XpsDocument.");
        ////}
        ////else if (ex is System.Windows.Xps.XpsSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("The exception that is thrown for XML Paper Specification (XPS) document serialization errors.");
        ////}
        ////else if (ex is System.Windows.Xps.XpsWriterException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a method of either an System.Windows.Xps.XpsDocumentWriter or a System.Windows.Xps.VisualsToXpsDocument object is called that is incompatible with the current state of the object.");
        ////}
        ////else if (ex is System.Workflow.Activities.EventDeliveryFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("an event that is raised from the host cannot be delivered to the workflow instance. Typically the event is raised from an System.Workflow.Activities.ExternalDataExchangeService on a workflow instance. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Workflow.Activities.WorkflowAuthorizationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("role validation fails due to a specified identity that is not contained in the System.Workflow.Activities.WorkflowRoleCollection.");
        ////}
        ////else if (ex is System.Workflow.Activities.Rules.RuleEvaluationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents the base class for all exceptions caused by rule evaluation issues.");
        ////}
        ////else if (ex is System.Workflow.Activities.Rules.RuleEvaluationIncompatibleTypesException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("attempting to apply an operator to incompatible operands when you evaluate a rule.");
        ////}
        ////else if (ex is System.Workflow.Activities.Rules.RuleException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents the base class for all exceptions caused by evaluation or validation of rules.");
        ////}
        ////else if (ex is System.Workflow.Activities.Rules.RuleSetValidationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("processing cannot continue because a rule set cannot be validated.");
        ////}
        ////else if (ex is System.Workflow.ComponentModel.WorkflowTerminatedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents the System.Exception that is raised when a workflow is terminated. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Workflow.ComponentModel.Compiler.WorkflowValidationFailedException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Represents an System.Exception that occurs when a workflow does not pass validation. This class cannot be inherited.");
        ////}
        ////else if (ex is System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("methods that perform serialization and deserialization.");
        ////}
        ////else if (ex is System.Workflow.Runtime.WorkflowOwnershipException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the workflow runtime engine attempts to load a workflow instance that is currently loaded by another workflow runtime engine instance. Additionally, this exception is thrown when the workflow runtime engine attempts to save a workflow after the ownership timeout that was specified while loading the workflow has expired.");
        ////}
        ////else if (ex is System.Workflow.Runtime.Hosting.PersistenceException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("the persistence service cannot fulfill a request.");
        ////}
        ////else if (ex is System.Workflow.Runtime.Tracking.TrackingProfileDeserializationException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("an XML document cannot be deserialized into a System.Workflow.Runtime.Tracking.TrackingProfile by a System.Workflow.Runtime.Tracking.TrackingProfileSerializer.");
        ////}
        ////else if (ex is System.Xaml.XamlDuplicateMemberException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a XAML writer attempts to write a value for a duplicate member into the same object node.");
        ////}
        ////else if (ex is System.Xaml.XamlException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("The exception that is thrown for a general XAML reader or XAML writer exception. See Remarks.");
        ////}
        ////else if (ex is System.Xaml.XamlInternalException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("The exception that is thrown for internal inconsistencies that occur during XAML reading and XAML writing.");
        ////}
        ////else if (ex is System.Xaml.XamlObjectReaderException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("several System.Xaml.XamlObjectReader internal helper APIs.");
        ////}
        ////else if (ex is System.Xaml.XamlObjectWriterException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a XAML writer (such as the System.Xaml.XamlObjectWriter class) encounters an error while attempting to produce object graphs from a XAML node stream.");
        ////}
        ////else if (ex is System.Xaml.XamlParseException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a XAML reader cannot process elements of the XAML reader source into a XAML node stream.");
        ////}
        ////else if (ex is System.Xaml.XamlSchemaException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("a binding system or another schema representation system for XAML reports an exception to the schema context.");
        ////}
        ////else if (ex is System.Xaml.XamlXmlWriterException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("certain System.Xaml.XamlXmlWriter APIs.");
        ////}
        ////else if (ex is Microsoft.Build.BuildEngine.InternalLoggerException)
        ////{////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("This exception is used to wrap an unhandled exception from a logger.");
        ////}
        ////else if (ex is Microsoft.Build.BuildEngine.InvalidProjectFileException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("This exception is thrown whenever there is a problem with the user's XML project file. The problem might be semantic or syntactical. If the problem is in the syntax, it can typically be caught by XSD validation.");
        ////}
        ////Exceptionsクラス非対応のためコメントアウト
        ////else if (ex is Microsoft.Build.BuildEngine.InvalidToolsetDefinitionException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("This exception is thrown whenever there is a problem with the user's custom toolset definition file. The problem might be semantic or syntactical. If the problem is in the syntax, it can typically be caught by XSD validation.");
        ////
        ////else if (ex is Microsoft.Build.BuildEngine.RemoteErrorException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Wraps exceptions that occur on a different node.");
        ////}
        ////else if (ex is Microsoft.Build.Exceptions.BuildAbortedException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("An exception representing the case where the build was aborted by request, as opposed to being unceremoniously shut down due to another kind of error exception.");
        ////}
        ////else if (ex is Microsoft.Build.Exceptions.InternalLoggerException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("This exception is used to wrap an unhandled exception from a logger. This exception aborts the build, and it can only be thrown by the MSBuild engine.");
        ////}
        ////else if (ex is Microsoft.Build.Exceptions.InvalidProjectFileException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("This exception is thrown whenever there is a problem with the user's XML project file. The problem might be semantic or syntactical. The latter would be of a type typically caught by XSD validation (if it was performed by the project writer).");
        ////}
        ////else if (ex is Microsoft.Build.Exceptions.InvalidToolsetDefinitionException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Exception subclass that ToolsetReaders should throw.");
        ////}
        ////else if (ex is Microsoft.Build.Framework.LoggerException)
        ////{
        ///////Exceptionsクラス非対応のためコメントアウト
        ////    Logger.Error("Allows a logger to force the build to stop in an explicit way.");
        ////}
        ////else if (ex is Microsoft.JScript.CmdLineException)
        ////{
        ////    Logger.Error("Represents errors that occur when you run the command-line compiler jsc.exe.");
        ////}
        ////else if (ex is Microsoft.JScript.JScriptException)
        ////{
        ////    Logger.Error("JScript to notify a common language runtime (CLR) host or program that an error occurred. A Microsoft.JScript.JScriptException usually takes a Microsoft.JScript.JSError enumeration value.");
        ////}
        ////else if (ex is Microsoft.JScript.NoContextException)
        ////{
        ////    Logger.Error("there is no code Microsoft.JScript.Context associated with a Microsoft.JScript.JScriptException.");
        ////}
        ////else if (ex is Microsoft.JScript.ParserException)
        ////{
        ////    Logger.Error("This class is used by the JScript parser to represent parser exceptions.");
        ////}
        ////else if (ex is Microsoft.JScript.Vsa.JSVsaException)
        ////{
        ////    Logger.Error("This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.");
        ////}
        ////else if (ex is Microsoft.Management.Infrastructure.CimException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Cmdletization.Cim.CimJobException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.CertificateNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.CertificateProviderItemNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.CertificateStoreLocationNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.CertificateStoreNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.HelpCategoryInvalidException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.HelpNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.ProcessCommandException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.RestartComputerTimeoutException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.ServiceCommandException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.WriteErrorException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.StringManipulation.FlashExtractWrapper.FlashExtract.ProgramNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.StringManipulation.FlashExtractWrapper.FlashExtract.ResultNotFoundException)
        ////{
        ////    Logger.Error("");
        ////}
        ////else if (ex is Microsoft.PowerShell.Commands.StringManipulation.FlashExtractWrapper.TemplateParsing.TemplateParsingException)
        ////{
        ////    Logger.Error("");
        ////}

        ////else if (ex is Microsoft.VisualBasic.ApplicationServices.CantStartSingleInstanceException)
        ////{
        ////    Logger.Error("This exception is thrown when a subsequent instance of a single-instance application is unable to connect to the first application instance.");
        ////}
        ////else if (ex is Microsoft.VisualBasic.ApplicationServices.NoStartupFormException)
        ////{
        ////    Logger.Error("This exception is thrown by the Visual Basic Application Model when the Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.MainForm property has not been set.");
        ////}
        ////else if (ex is Microsoft.VisualBasic.CompilerServices.InternalErrorException)
        ////{
        ////    Logger.Error("The exception thrown for internal Visual Basic compiler errors.");
        ////}











    }


}
