using System;
using System.Diagnostics;

namespace error
{
    public class ExceptionUI : ExceptionBase
    {

        /// <summary>
        /// Handle exception in user interface tools
        /// </summary>
        /// <param name="ex">Generic Exception</param>
        /// <param name="UIMessage">Optional main user interface message for form</param>
        /// <remarks></remarks>

        public static void HandleException(System.Exception ex, string UIMessage)
        {
            string sMessage = GetExceptionInformation(ex);
            sMessage += "Windows: " + Environment.OSVersion + Environment.NewLine;
            sMessage += "Date: " + DateTime.Now.ToString() + Environment.NewLine;
            //
            // Ensure the wait cursor is reverted back to the default cursor before showing any message box
            //
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            //if the exception has a UIMessage parameter, it overrides the optional UIMessage passed to the method
            if (ex.Data.Contains("UIMessage"))
            {
                UIMessage = ex.Data["UIMessage"].ToString();
            }

            Debug.WriteLine(sMessage);
            Debug.WriteLine(DateTime.Now);
            frmException myFrm = new frmException(string.Empty, sMessage);
            myFrm.ShowDialog();
        }

        public static void HandleException(Exception ex)
        {
            ExceptionUI.HandleException(ex, "");
        }
    }
}
