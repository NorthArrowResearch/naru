namespace naru.ui
{
    public class UIHelpers
    {
        public static string WrapMessageWithNoun(string sMessageStart, string sNoun, string sMessageEnd)
        {
            string sResult = sMessageStart;
            if (!string.IsNullOrEmpty(sNoun))
            {
                sNoun = sNoun.Trim();

                if (sNoun.ToLower().StartsWith("a") || sNoun.ToLower().StartsWith("e") || sNoun.ToLower().StartsWith("i") || sNoun.ToLower().StartsWith("o") || sNoun.ToLower().StartsWith("u"))
                {
                    sMessageStart = sMessageStart.Trim();
                    string sStartSuffix = string.Empty;
                    if (sMessageStart.EndsWith(" a"))
                    {
                        sMessageStart += "n";
                    }
                }
                sResult += " " + sNoun + " ";
            }
            sResult += " " + sMessageEnd.Trim();

            return sResult;
        }
    }
}
