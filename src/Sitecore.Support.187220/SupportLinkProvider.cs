using Sitecore.Links;

namespace Sitecore.Support
{
    public class SupportLinkProvider : LinkProvider
    {
        #region Modified
        protected override LinkBuilder CreateLinkBuilder(UrlOptions options)
        {
            return new SupportLinkBuilder(options);
        }
        #endregion
    }
}