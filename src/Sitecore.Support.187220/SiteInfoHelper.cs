using System;
using System.Collections.Generic;
using Sitecore.Diagnostics;
using Sitecore.Text;
using Sitecore.Web;

namespace Sitecore.Support
{
    public static class SiteInfoHelper
    {
        // Uses instead of SiteInfo.Matces(string host) to match hosts
        public static bool Matches(string host, SiteInfo siteInfo)
        {
            Assert.ArgumentNotNull(host, "host");
            if ((host.Length == 0) || (siteInfo.HostName.Length == 0))
            {
                return true;
            }
            host = host.ToLowerInvariant();
            foreach (string[] strArray in GetHostNamePattern(siteInfo))
            {
                if (SupportWildCardParser.Matches(host, strArray))
                {
                    return true;
                }
            }
            return false;
        }

        // Uses to get hostNamePattern field that private in SiteInfo
        private static IList<string[]> GetHostNamePattern(SiteInfo siteInfo)
        {
            IList <string[]> hostNamePatterns = new List<string[]>();
            hostNamePatterns = new List<string[]>();
            foreach (string str in siteInfo.HostName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                hostNamePatterns.Add(WildCardParser.GetParts(str));
            }
            return hostNamePatterns;
        }
    }
}