using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Survey.Model.Models;
using Survey.Service;
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
    }
}
