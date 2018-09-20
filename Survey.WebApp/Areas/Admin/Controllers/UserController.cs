using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Survey.Model.Models;
using Survey.Service;
using Survey.WebApp.Helpers.Extensions;
using Survey.WebApp.Models;

namespace Survey.WebApp.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(string q,
                                    int offset,
                                    int limit)
        {
            int total;
            if(!string.IsNullOrEmpty(q))
            {
                q = q.Trim()
                     .ToLower();
            }

            IEnumerable<UserModel> query = Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(_userService.GetMultiPaging(c => string.IsNullOrEmpty(q) || c.Name.ToLower()
                                                                                                                                                              .Contains(q) || c.Username.ToLower()
                                                                                                                                                                               .Contains(q),
                                                                                                                             c => c.OrderBy(v => v.Name),
                                                                                                                             out total,
                                                                                                                             offset,
                                                                                                                             limit));
            return Json(new FormatResult<UserModel>(total,
                                                    query),
                        "json");
        }

        public ActionResult Add()
        {
            return View(new CreateUserModel());
        }

        [HttpPost]
        public ActionResult Add(CreateUserModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new User();
                user.UpdateModel(model);
                var result = _userService.Register(user);
                if(result.State == 1)
                {
                    TempData["Update"] = Server.FormatResultToView(result);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Username",
                                         result.KeyLanguage);
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if(user != null)
            {
                return View(Mapper.Map<User, UpdateUserModel>(user));
            }

            return View("Notfound");
        }

        [HttpPost]
        public ActionResult Edit(UpdateUserModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _userService.GetById(model.Id);
                if(user != null)
                {
                    user.UpdateModel(model);
                    var result = _userService.Update(user);
                    if(result.State == 1)
                    {
                        TempData["Update"] = Server.FormatResultToView(result);
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("Username",
                                             result.KeyLanguage);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            return Json(Server.FormatResultToJson(result));
        }
    }
}
