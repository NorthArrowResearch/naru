using System;
using System.Diagnostics;

namespace naru.error
{
    public class ExceptionUI : ExceptionBase
    {
        /// <summary>
        /// Handle exception in user interface tools
        /// </summary>
        /// <param name="ex">Generic Exception</param>
        /// <param name="UIMessage">Optional main user interface message for form</param>
        /// <remarks></remarks>
        public static void HandleException(Exception ex, string UIMessage, string newIssueURL)
        {
            string formattedException = GetExceptionInformation(ex);
            formattedException += Environment.NewLine + "Windows: " + Environment.OSVersion;
            formattedException += Environment.NewLine + "Date: " + DateTime.Now.ToString();
            //
            // Ensure the wait cursor is reverted back to the default cursor before showing any message box
            //
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            //if the exception has a UIMessage parameter, it overrides the optional UIMessage passed to the method
            if (ex.Data.Contains("UIMessage"))
            {
                UIMessage = ex.Data["UIMessage"].ToString();
            }

            if (string.IsNullOrEmpty(UIMessage))
                UIMessage = ex.Message;

            Debug.WriteLine(formattedException);
            Debug.WriteLine(DateTime.Now);
            frmException myFrm = new frmException(UIMessage, formattedException, newIssueURL);
            myFrm.ShowDialog();
        }
    }
}
