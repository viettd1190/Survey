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
    }
}
