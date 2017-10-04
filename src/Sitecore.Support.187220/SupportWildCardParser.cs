using System;

namespace Sitecore.Support
{
    public static class SupportWildCardParser
    {
        #region Modified
        public static bool Matches(string value, string[] matchParts)
        {
            if ((value.Length > 0) && (matchParts.Length > 0))
            {
                bool flag = false;
                for (int i = 0; i < matchParts.Length; i++)
                {
                    string str = matchParts[i];
                    if (str.Length > 0)
                    {
                        if (str[0] == '*')
                        {
                            flag = true;
                        }
                        else
                        {
                            int index = value.IndexOf(str, StringComparison.InvariantCulture);
                            if ((index < 0) || ((index > 0) && !flag))
                            {
                                return false;
                            }
                            value = value.Substring(index + str.Length);
                            // Added flag = false;
                            flag = false;
                        }
                    }
                }
                // Added this condition to avoid missing ends of host name
                if (value.Length != 0 && !flag)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

    }
}