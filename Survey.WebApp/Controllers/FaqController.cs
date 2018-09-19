using System.Collections.Generic;
using System.Web.Mvc;
using Survey.Service;
using Survey.WebApp.Models;

namespace Survey.WebApp.Controllers
{
    public class FaqController : BaseController
    {
        private IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: Faq
        public ActionResult Index(int? page)
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(string q,
                                    int pageNumber,
                                    int pageSize)
        {
            List<FaqModel> list = new List<FaqModel>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new FaqModel
                         {
                                 Id = i + 1,
                                 Title = "HƯỚNG DẪN ĐẶT HÀNG",
                                 Content = "Đế Sạc Không Dây Tronsmart WC01 Chuẩn Qi 10W Type-C (Không Kèm Adapter) tương thích với công nghệ sạc không dây Qi: Hỗ trợ hầu hết các điện thoại thông minh."
                         });
            }

            return Json(list,
                        "json");
        }
    }
}
