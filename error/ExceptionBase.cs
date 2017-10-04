using System;

namespace error
{
    public abstract class ExceptionBase
    {
        /// <summary>
        /// Date time format string that complies with ISO 8601
        /// </summary>
        /// <remarks>Link to ISO
        /// https://en.wikipedia.org/wiki/ISO_8601
        /// http://stackoverflow.com/questions/114983/given-a-datetime-object-how-do-i-get-a-iso-8601-date-in-string-format
        /// The explicit format string for this is "yyyy-MM-ddTHH\:mm\:sszzz"
        /// </remarks>

        public const string DateTimeFormat = "O";
        /// <summary>
        /// Recursive function that appends exception data and inner exceptions to the exception message
        /// </summary>
        /// <param name="ex">Exception to process</param>
        /// <remarks>Note that this method is recursive</remarks>
        protected static string GetExceptionInformation(Exception ex)
        {
            string sMessage = "";
            if (ex.InnerException is Exception)
            {
                // At least one more exception level to report, so make it clear where the break is.
                sMessage += "------------------------------------------------";
                sMessage += Environment.NewLine + "EXCEPTION";
            }
            sMessage += Environment.NewLine + ex.Message;
            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                sMessage += Environment.NewLine + " --- Stacktrace --- ";
                sMessage += Environment.NewLine + ex.StackTrace;
            }

            if (ex.Data.Contains("Parameters"))
            {
                sMessage += Environment.NewLine + " --- Parameters --- ";
                foreach (System.Collections.DictionaryEntry de in ex.Data)
                {
                    sMessage += Environment.NewLine + " " + de.Key + " = " + de.Value;
                }
            }

            if (ex.Data.Count > 0)
            {
                sMessage += Environment.NewLine + " --- Exception Data --- ";
                foreach (System.Collections.DictionaryEntry de in ex.Data)
                {
                    if ((de.Key != null))
                    {
                        sMessage += Environment.NewLine + " " + de.Key.ToString() + " = ";
                        if (de.Value == null)
                        {
                            sMessage += "Nothing";
                        }
                        else
                        {
                            sMessage += de.Value.ToString();
                        }
                    }
                }
            }

            if (ex.InnerException is Exception)
            {
                sMessage += GetExceptionInformation(ex.InnerException);
            }
            return sMessage;
        }
    }
}