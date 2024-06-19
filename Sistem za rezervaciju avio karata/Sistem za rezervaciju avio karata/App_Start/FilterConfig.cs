using System.Web;
using System.Web.Mvc;

namespace Sistem_za_rezervaciju_avio_karata
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
