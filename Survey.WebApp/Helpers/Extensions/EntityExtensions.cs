using System;
using Survey.Model.Models;
using Survey.WebApp.Models;

namespace Survey.WebApp.Helpers.Extensions
{
    public static partial class Extensions
    {
        public static void UpdateModel(this Faq faq,
                                       FaqModel model)
        {
            faq.Id = model.Id;
            faq.Title = model.Title;
            faq.Content = model.Content;
            faq.IsDisplay = model.IsDisplay;
        }

        public static void UpdateModel(this User user,
                                       CreateUserModel model)
        {
            user.Name = model.Name;
            user.Password = model.Password;
            user.Origanization = model.Origanization;
            user.Specification = model.Specification;
            DateTime fromDate;
            if(DateTime.TryParse(model.FromDate,
                                 out fromDate))
            {
                user.FromDate = fromDate;
            }

            user.Username = model.Username;
            user.Role = model.Role;
            user.Status = model.Status;
        }

        public static void UpdateModel(this User user,
                                       UpdateUserModel model)
        {
            user.Id = model.Id;
            user.Name = model.Name;
            if(!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }
            user.Origanization = model.Origanization;
            user.Specification = model.Specification;
            DateTime fromDate;
            if(DateTime.TryParse(model.FromDate,
                                 out fromDate))
            {
                user.FromDate = fromDate;
            }

            user.Username = model.Username;
            user.Role = model.Role;
            user.Status = model.Status;
        }
    }
}
