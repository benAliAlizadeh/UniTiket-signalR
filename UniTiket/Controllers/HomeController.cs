using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniTiket.Models;
using UniTiket.Repositories;
using UniTiKet_Model;
using Microsoft.AspNetCore.Authorization;
using UniTiket.Tools;

namespace UniTiket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _db;
        private readonly ICategoryRepository _cr;
        private readonly IMessageRepository _mr;
        private readonly ITiketRepository _tr;
        private readonly HttpContext _httpContext;

        public HomeController(ILogger<HomeController> logger, IUserRepository db, IHttpContextAccessor httpContext, ICategoryRepository cr, IMessageRepository mr, ITiketRepository tr)
        {
            _logger = logger;
            _db = db;
            _httpContext = httpContext.HttpContext;
            _cr = cr;
            _mr = mr;
            _tr = tr;
        }

        int GetUserId()
        {
            //return 1;
            return int.Parse(_httpContext.User.FindFirst("UserId").Value);
        }
        bool IsAdmin()
        {
            return _httpContext.User.IsInRole("Admin");
        }

        [Authorize]
        [Route("/{id?}")]
        public async Task<IActionResult> Index(int id)
        {
            
            int userId = GetUserId();
            var user = await _db.FindByIdAsync(userId);
            List<TiketViewModel> tikets = new();

            if (IsAdmin())
            {
                if (user.CategoryId > 0)
                {
                    tikets = await _tr.GetAdminTiketsAsync((int)user.CategoryId);
                    id = (id > 0) ? id : tikets.FirstOrDefault().TiketId;
                }
                else
                {
                    tikets = await _tr.GetAllTiketsAsync();
                    id = (id > 0) ? id : tikets.FirstOrDefault().TiketId;
                }

            }
            else
            {

                tikets = await _tr.GetTiketsAsync(userId);

                if (tikets.Count <= 0)
                {
                    return Redirect("/Home/CreateTiket");
                }
                id = (id > 0) ? id : tikets.FirstOrDefault().TiketId;
            }


            var tiket = await _tr.GetTiketById(id);

            if (tiket.UserId != GetUserId() && !IsAdmin())
            {
                return NotFound();
            }

            ViewBag.tiketId = id;


            var messages = await _mr.GetMessages(id);

            //List<TiketViewModel> tiketsV = new();
            //foreach (var item in tikets)
            //{
            //    string lastM = messages.FirstOrDefault(c=> c.TiketId == item.TiketId).Text;
            //    tiketsV.Add(new()
            //    {
            //        CategoryId = item.CategoryId,
            //        CreatedTime = item.CreatedTime,
            //        TiketId = item.TiketId,
            //        Title = item.Title,
            //        UserId = item.UserId,
            //        LastMessage = lastM
            //    });
            //}

            return View(new ChatViewModel()
            {
                Messages = messages,
                Tikets = tikets,
                Tiket = tiket
            });
        }

        [Authorize]
        public async Task<IActionResult> CreateTiket()
        {
            return View(new CTiketViewModel()
            {
                Categories = await GetCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTiket(CTiketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var tiket = await _tr.AddAsync(new()
            {
                CategoryId = int.Parse(model.Category),
                CreatedTime = DateTime.Now,
                Title = model.Title,
                UserId = GetUserId()
            });
            await _tr.SaveChangesAsync();

            await _mr.AddAsync(new()
            {
                CreatedTime = DateTime.Now,
                IsAnser = false,
                TiketId = tiket.TiketId,
                Text = model.Text
            });
            await _mr.SaveChangesAsync();

            return Redirect($"/{tiket.TiketId}");
        }
        async Task<List<Category>> GetCategories() => (await _cr.GetAllAsync()).ToList();

        [Authorize]
        public async Task<IActionResult> EditTiket(int id)
        {
            var tiket = await _tr.GetTiketById(id);
            return View(new ETiketViewModel()
            {
                Categories = await GetCategories(),
                Category = tiket.CategoryId.ToString(),
                IsFinaly = tiket.IsFinaly,
                Title = tiket.Title,
                TiketId = tiket.TiketId
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditTiket(ETiketViewModel model)
        {
            var tiket = await _tr.GetTiketById(model.TiketId);

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                model.TiketId = tiket.TiketId;
                model.Title = model.Title;
                model.Category = tiket.CategoryId.ToString();
                model.IsFinaly = tiket.IsFinaly;

                return View(model);
            }

            tiket.Title = model.Title;
            tiket.IsFinaly=model.IsFinaly;
            tiket.CategoryId = int.Parse(model.Category);

            await _tr.UpdateAsync(tiket);

            await _tr.SaveChangesAsync();



            return Redirect($"/{tiket.TiketId}");
        }


        #region Account
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _db.GetUserAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("Password", "UserName Or Password is wrong");
                return View(model);
            }

            await UserLogin(user);

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            if (_httpContext.User.Identity is { IsAuthenticated: true })
            {
                await _httpContext.SignOutAsync();
            }
            return Redirect("/");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _db.IsExist(model.Username))
            {
                ModelState.AddModelError("Username", "This UserName Has Exist");
                return View(model);
            }

            var user = await _db.AddAsync(new()
            {
                UserName = model.Username,
                Password = model.Password
            });
            await _db.SaveChangesAsync();

            await UserLogin(user);

            return Redirect("/");
        }

        private async Task UserLogin(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, (user.IsAdmin)? "Admin" : "User"),
                new Claim("UserId", user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var principal = new ClaimsPrincipal(claimsIdentity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true
            };
            await _httpContext.SignInAsync(principal, properties);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}