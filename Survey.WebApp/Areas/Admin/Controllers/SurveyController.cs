using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.WebApp.Models;

namespace Survey.WebApp.Areas.Admin.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Admin/Survey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View(new SurveyModel());
        }
    }
}