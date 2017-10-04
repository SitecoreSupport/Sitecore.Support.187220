using System;
using Sitecore.Links;
using Sitecore.Sites;
using Sitecore.Web;

namespace Sitecore.Support
{
    public class SupportLinkBuilder : LinkProvider.LinkBuilder
    {
        public SupportLinkBuilder(UrlOptions options): base(options)
        { }

        #region Original code
        internal virtual string GetHostName() =>
            WebUtil.GetHostName();

        internal virtual string GetScheme() =>
            WebUtil.GetScheme();
        #endregion

        #region Modified
        protected override string GetServerUrlElement(SiteInfo siteInfo)
        {
            SiteContext site = Context.Site;
            string str = (site != null) ? site.Name : string.Empty;
            string hostName = this.GetHostName();
            string str3 = this.AlwaysIncludeServerUrl ? WebUtil.GetServerUrl() : string.Empty;
            if (siteInfo == null)
            {
                return str3;
            }
            // Original siteInfo.Matches(hostName) replaced with SiteInfoHelper.Matches(hostName, siteInfo) to avoid incorrect work of WildCardParser class
            // Original code:
            // string str4 = ((!string.IsNullOrEmpty(siteInfo.HostName) && !string.IsNullOrEmpty(hostName)) && siteInfo.Matches(hostName)) ? hostName : StringUtil.GetString(new string[] { this.GetTargetHostName(siteInfo), hostName });
            string str4 = ((!string.IsNullOrEmpty(siteInfo.HostName) && !string.IsNullOrEmpty(hostName)) && SiteInfoHelper.Matches(hostName, siteInfo)) ? hostName : StringUtil.GetString(new string[] { this.GetTargetHostName(siteInfo), hostName });
            if ((!this.AlwaysIncludeServerUrl && siteInfo.Name.Equals(str, StringComparison.OrdinalIgnoreCase)) && hostName.Equals(str4, StringComparison.OrdinalIgnoreCase))
            {
                return str3;
            }
            if ((str4 == string.Empty) || (str4.IndexOf('*') >= 0))
            {
                return str3;
            }
            string str5 = StringUtil.GetString(new string[] { siteInfo.Scheme, this.GetScheme() });
            int @int = MainUtil.GetInt(siteInfo.Port, WebUtil.GetPort());
            int port = WebUtil.GetPort();
            string scheme = this.GetScheme();
            StringComparison ordinalIgnoreCase = StringComparison.OrdinalIgnoreCase;
            if ((str4.Equals(hostName, ordinalIgnoreCase) && (@int == port)) && str5.Equals(scheme, ordinalIgnoreCase))
            {
                return str3;
            }
            string str7 = str5 + "://" + str4;
            if ((@int > 0) && (@int != 80))
            {
                str7 = str7 + ":" + @int;
            }
            return str7;
        }
        #endregion
    }
}