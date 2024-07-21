using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_PRN211.Models;

namespace Project_PRN211.Controllers
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class HomeController : Controller
    {

        Project_PRNContext context = new Project_PRNContext();

        public IActionResult Home()
        {
            Account a = HttpContext.Session.GetObject<Account>("account");
            ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
            List<Account> accounts = context.Accounts.ToList();
            if (a != null)
            {
                for (int i = 0; i < accounts.Count(); i++)
                {
                    if (accounts[i].Email.Equals(a.Email))
                    {
                        accounts.Remove(accounts[i]);
                    }
                }
            }
            ViewBag.category = context.BlogCategories.ToList();
            ViewBag.login = a;
            return View(accounts);
        }

        public IActionResult Profile(int acid)
        {
            Account a = HttpContext.Session.GetObject<Account>("account");
            Account ac = context.Accounts.FirstOrDefault(x => x.AccountId == acid);
            List<Blog> blog = context.Blogs.Where(x => x.AccountId == acid).ToList();
            ViewBag.ac = ac;
            ViewBag.login = a;
            ViewBag.check = acid;
            ViewBag.category = context.BlogCategories.ToList();
            ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
            return View(blog);
        }
        public IActionResult Blog(string search, int cate)
        {
            if (search == null && cate == 0)
            {
                Account a = HttpContext.Session.GetObject<Account>("account");
                List<Blog> blog = context.Blogs.ToList();
                if (a != null)
                {
                    blog = context.Blogs.Where(x => x.AccountId != a.AccountId).ToList();
                }
                ViewBag.ac = context.Accounts.ToList();
                ViewBag.popular = context.Blogs.OrderByDescending(b => b.NumOfLike).Take(4).ToList();
                ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
                ViewBag.category = context.BlogCategories.ToList();
                return View(blog);
            }
            else if (cate != 0)
            {
                Account a = HttpContext.Session.GetObject<Account>("account");
                List<Blog> blog = context.Blogs.Where(x => x.CategoryId == cate).ToList();
                if (a != null)
                {
                    blog = context.Blogs.Where(x => x.AccountId != a.AccountId).ToList();
                }
                ViewBag.ac = context.Accounts.ToList();
                ViewBag.popular = context.Blogs.OrderByDescending(b => b.NumOfLike).Take(4).ToList();
                ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
                ViewBag.category = context.BlogCategories.ToList();
                return View(blog);
            }
            else
            {
                Account a = HttpContext.Session.GetObject<Account>("account");
                List<Blog> blog = context.Blogs.Where(x => x.Tilte.Contains(search)).ToList();
                if (a != null)
                {
                    blog = context.Blogs.Where(x => x.AccountId != a.AccountId).ToList();
                }
                ViewBag.ac = context.Accounts.ToList();
                ViewBag.popular = context.Blogs.OrderByDescending(b => b.NumOfLike).Take(4).ToList();
                ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
                ViewBag.category = context.BlogCategories.ToList();
                return View(blog);
            }
        }

        public IActionResult BlogDetail(int blogid)
        {
            Account a = HttpContext.Session.GetObject<Account>("account");
            Blog blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogid);
            List<Comment> comments = context.Comments.Where(x => x.BlogId == blogid).ToList();
            ViewBag.login = a;
            ViewBag.count = comments.Count;
            ViewBag.author = context.Accounts.FirstOrDefault(x => x.AccountId == blog.AccountId);
            ViewBag.blog = blog;
            ViewBag.ac = context.Accounts.ToList();
            ViewBag.popular = context.Blogs.OrderByDescending(b => b.NumOfLike).Take(4).ToList();
            ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
            ViewBag.category = context.BlogCategories.ToList();
            ViewBag.cmr = 0;
            return View(comments);
        }
        [HttpPost]
        public IActionResult BlogDetail(int blogid, string comment, int cmid)
        {
            Account a = HttpContext.Session.GetObject<Account>("account");
            Blog blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogid);
            ViewBag.login = a;
            ViewBag.author = context.Accounts.FirstOrDefault(x => x.AccountId == blog.AccountId);
            ViewBag.blog = blog;
            ViewBag.ac = context.Accounts.ToList();
            ViewBag.popular = context.Blogs.OrderByDescending(b => b.NumOfLike).Take(4).ToList();
            ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
            ViewBag.category = context.BlogCategories.ToList();
            Comment cm = new Comment()
            {
                BlogId = blogid,
                AccountId = a.AccountId,
                CommentDetail = comment,
                Commentdate = DateTime.Now,
                Isrep = cmid
            };          
            if (cm != null && cmid == 0)
            {
                context.Comments.Add(cm);
                context.SaveChanges();
            }
            List<Comment> comments = context.Comments.Where(x => x.BlogId == blogid).ToList();
            ViewBag.count = comments.Count;
            return View("BlogDetail", comments);
        }

        public IActionResult AddBlog()
        {
            Account a = HttpContext.Session.GetObject<Account>("account");
            ViewBag.login = a;
            ViewBag.category = context.BlogCategories.ToList();
            ViewBag.status = context.BlogStatuses.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(int acid, string tilte, int category, string detail, int status, string image)
        {
            DateTime today = DateTime.Now;
            Blog bloga = new Blog()
            {
                AccountId = acid,
                CategoryId = category,
                Tilte = tilte,
                BlogDetail = detail,
                UpdateDate = today,
                StatusId = status,
                NumOfLike = 0,
                Image = image
            };
            if (bloga != null)
            {
                context.Blogs.Add(bloga);
                context.SaveChanges();
            }
            Account a = HttpContext.Session.GetObject<Account>("account");
            Account ac = context.Accounts.FirstOrDefault(x => x.AccountId == acid);
            List<Blog> blog = context.Blogs.Where(x => x.AccountId == acid).ToList();
            ViewBag.ac = ac;
            ViewBag.login = a;
            ViewBag.check = acid;
            ViewBag.category = context.BlogCategories.ToList();
            ViewBag.newfeed = context.Blogs.OrderByDescending(b => b.UpdateDate).Take(2).ToList();
            return View("Profile", blog);
        }
        public IActionResult Login()
        {
            return View();
        }
        
        static int count =0;
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            if (account != null && count <1)
            {
                HttpContext.Session.SetObject("account", account);
                return RedirectToAction("Home", account);
            }
            else if(account == null && count < 1)
            {
                count++;
                ViewBag.error = "Your password is incorect. Please enter again!";
                return View("Login");
            }
            else
            {
                ViewBag.check = "khong cho";
				return View("Login");
			}
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home");
        }

        public string checkDup(string email)
        {
            string check = null;
            if (!string.IsNullOrEmpty(email))
            {
                Account a = context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                if (a == null)
                {
                    check = "Email đã tồn tại";
                }
            }
            return check;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string email, string password, string name, string gender, string mobile, string address)
        {
            string check = checkDup(email);
            if (check == null)
            {
                ViewBag.error = check;
            }
            else
            {
                Boolean hi = true;
                if (gender.Equals("Female"))
                {
                    hi = false;
                }
                Account a = new Account()
                {
                    Email = email,
                    Password = password,
                    Name = name,
                    Gender = hi,
                    Mobile = mobile,
                    Address = address
                };
                if (ModelState.IsValid)
                {
                    context.Accounts.Add(a);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


    }
}
