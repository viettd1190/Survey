using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Survey.Model.Abstract;
using Survey.Model.Models;
using Survey.Service;
using Survey.WebApp.Helpers.Extensions;
using Survey.WebApp.Models;

namespace Survey.WebApp.Areas.Admin.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        // GET: Admin/Faq
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(int offset,
                                    int limit)
        {
            int total;
            IEnumerable<FaqModel> query = Mapper.Map<IEnumerable<Faq>, IEnumerable<FaqModel>>(_faqService.GetMultiPaging(c => c.Id > 0,
                                                                                                                         c => c.OrderBy(v => v.Title)
                                                                                                                               .ThenByDescending(v => v.CreatedDate),
                                                                                                                         out total,
                                                                                                                         offset,
                                                                                                                         limit));
            return Json(new FormatResult<FaqModel>(total,
                                                   query),
                        "json");
        }

        public ActionResult Add()
        {
            return View(new FaqModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FaqModel model)
        {
            if(ModelState.IsValid)
            {
                Faq faq = new Faq();
                faq.UpdateModel(model);
                UpdateResult result = _faqService.Add(faq);
                if(result.State == 1)
                {
                    ViewBag.Completed = true;
                }
                else
                {
                    ViewBag.Completed = false;
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Faq faq = _faqService.GetById(id);
            if(faq != null)
            {
                return View(Mapper.Map<Faq, FaqModel>(faq));
            }
            return View("NotfoundDialog");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FaqModel model)
        {
            if(ModelState.IsValid)
            {
                Faq faq = _faqService.GetById(model.Id);
                if(faq != null)
                {
                    faq.UpdateModel(model);
                    UpdateResult result = _faqService.Update(faq);
                    if(result.State == 1)
                    {
                        ViewBag.Completed = true;
                    }
                    else
                    {
                        ViewBag.Completed = false;
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UpdateResult result = _faqService.Delete(id);
            return Json(Server.FormatResultToJson(result));
        }
    }
}
