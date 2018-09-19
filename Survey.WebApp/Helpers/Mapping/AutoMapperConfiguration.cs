using AutoMapper;
using Survey.Common;
using Survey.Model.Models;
using Survey.WebApp.Models;

namespace Survey.WebApp.Helpers.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Faq, FaqModel>();
            Mapper.CreateMap<User, UserModel>()
                  .ForMember(c => c.LastLoginDate,
                             v => v.ResolveUsing(src => src.LastLoginDate == null
                                                            ? string.Empty
                                                            : string.Format(AppUtil.DATE_TIME_PARAM_FORMAT,
                                                                            src.LastLoginDate)));
        }
    }
}
