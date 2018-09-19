using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Survey.Model.Models;
using Survey.Service;
using Survey.WebApp.Models;

namespace Survey.WebApp.Controllers
{
    public class FaqController : BaseController
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: Faq
        public ActionResult Index(int? page,
                                  int pageSize = 10)
        {
            if(page == null)
            {
                page = 1;
            }

            int total;
            IEnumerable<FaqModel> query = Mapper.Map<IEnumerable<Faq>, IEnumerable<FaqModel>>(_faqService.GetMultiPaging(c => c.IsDisplay,
                                                                                                                         c => c.OrderByDescending(v => v.CreatedDate),
                                                                                                                         out total,
                                                                                                                         page ?? 1,
                                                                                                                         pageSize));

            ViewBag.Page = page;
            ViewBag.Total = total;
            ViewBag.TotalPage = total % pageSize == 0
                                    ? total / pageSize
                                    : total / pageSize + 1;
            ViewBag.List = query.ToList();
            return View();
        }
    }
}
