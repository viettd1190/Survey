using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Survey.Model.Abstract;

namespace Survey.WebApp.Helpers.Extensions
{
    public static partial class Extensions
    {
        public static dynamic FormatResultToJson(this HttpServerUtilityBase httpServerUtilityBase,
                                                 UpdateResult result)
        {
            return new
                   {
                       lv = result.State,
                       msg = FormatResult(httpServerUtilityBase,
                                          result)
                   };
        }

        private static string FormatResult(this HttpServerUtilityBase httpServerUtilityBase,
                                           UpdateResult result)
        {
            JArray array = new JArray
                           {
                               new JValue(httpServerUtilityBase.GetResource(result.KeyLanguage))
                           };
            if(result.KeyLanguages != null)
            {
                foreach (string key in result.KeyLanguages)
                {
                    array.Add(new JValue(httpServerUtilityBase.GetResource(key)));
                }
            }

            return array.ToString(Formatting.None);
        }

        public static string GetResource(this HttpServerUtilityBase httpServerUtilityBase,
                                         string key)
        {
            if(!string.IsNullOrEmpty(key))
            {
                if("updatesuccessful".Equals(key,
                                             StringComparison.CurrentCultureIgnoreCase))
                {
                    return "Cập nhật thành công";
                }

                if("errorwhenupdated".Equals(key,
                                             StringComparison.CurrentCultureIgnoreCase))
                {
                    return "Xảy ra lỗi khi cập nhật";
                }

                if("informationhasexists".Equals(key,
                                                 StringComparison.CurrentCultureIgnoreCase))
                {
                    return "Thông tin đã tồn tại";
                }

                return key;
            }

            return string.Empty;
        }

        public static dynamic FormatResultToView(this HttpServerUtilityBase httpServerUtilityBase,
                                                 UpdateResult result)
        {
            return JsonConvert.SerializeObject(FormatResultToJson(httpServerUtilityBase,
                                                                  result),
                                               Formatting.None);
        }
    }
}
