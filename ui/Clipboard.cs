using System.Windows.Forms;

namespace naru.ui
{
    public struct Clipboard
    {
        /// <summary>
        /// Copy text to the clipboard
        /// </summary>
        /// <param name="sText">The data to place on the Clipboard.</param>
        /// <param name="bKeepAfterAppExits">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
        /// <param name="nRetries">The number of times to attempt placing the data on the Clipboard.</param>
        /// <param name="nDelay">The number of milliseconds to pause between attempts.</param>
        public static void SetText(string sText, bool bKeepAfterAppExits = false, int retryTimes = 5, int retryDelay = 200)
        {
            System.Windows.Forms.Clipboard.SetDataObject(sText, bKeepAfterAppExits, retryTimes, retryDelay);
        }
    }
}
