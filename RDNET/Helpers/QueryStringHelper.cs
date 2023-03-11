using System.Collections.Specialized;
using System.Web;

namespace RDNET;

internal static class QueryStringHelper
{
    internal static String ToQueryString(this NameValueCollection nameValueCollection)
    {
        var list = new List<String>();

        foreach (var key in nameValueCollection.AllKeys)
        {
            var values = nameValueCollection.GetValues(key);

            if (values != null)
            {
                foreach (var value in values)
                {
                    list.Add($"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}");
                }
            }
        }

        if (list.Count == 0)
        {
            return "";
        }

        return "?" + String.Join("&", list);
    }
}