namespace naru.ui
{
    public class UIHelpers
    {
        public static string WrapMessageWithNoun(string sMessageStart, string sNoun, string sMessageEnd)
        {
            sMessageStart = sMessageStart.Trim();

            if (!string.IsNullOrEmpty(sNoun))
            {
                sNoun = sNoun.Trim();

                if (sMessageStart.ToLower().EndsWith("a") && System.Text.RegularExpressions.Regex.IsMatch(sNoun, "^[aeiou]", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    sMessageStart += "n";
            }

            string sResult = string.Format("{0} {1} {2}", sMessageStart, sNoun, sMessageEnd).Replace("  ", " ");
            return sResult;
        }
    }
}
