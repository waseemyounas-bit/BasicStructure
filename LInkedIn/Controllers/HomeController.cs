using Business.IServices;
using Data.Context;
using Data.Entities;
using LInkedIn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LInkedIn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IUserService userService;
        private readonly IEducationService educationService;
        private readonly IConnectionService connectionService;
        private readonly IPostService postService;
        private readonly IExperienceService experienceService;
        private readonly ILicenseOfCertificationService licenseOfCertificationService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        public HomeController(ILogger<HomeController> logger,DataContext dataContext,IEducationService educationService,IUserService _userService, IConnectionService connectionService, IPostService postService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IExperienceService experienceService, ILicenseOfCertificationService licenseOfCertificationService)
        {
            userService = _userService;
            _logger = logger;
            this.connectionService = connectionService;
            this.postService = postService;
            this.hostingEnvironment = hostingEnvironment;
            this.experienceService = experienceService;
            this.licenseOfCertificationService = licenseOfCertificationService;
            this.educationService= educationService;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentUser")==null)
            {
                return RedirectToAction("Login","Account");
            }
            string json = HttpContext.Session.GetString("CurrentUser");
            User user = JsonSerializer.Deserialize<User>(json);
            user.Educations = educationService.GetAllEducation().Where(u => u.UserId == user.Id).ToList();
            user.Experiences=experienceService.GetAllExperience().Where(u => u.UserId == user.Id).ToList();
            user.LicenseOfCertifications=licenseOfCertificationService.GetAllLicenseOfCertification().Where(u => u.UserId == user.Id).ToList();
            user.connectionCount=connectionService.GetAllConnections().Where(u=>u.UserId==user.Id).Count();
            ViewData["jobs"]=postService.GetAllJobs().ToList();
            ViewData["posts"] = postService.GetAllPosts().Where(x=>x.IsJob==false).Include(x=>x.User).ToList();
            
            
            List<User> newfriends=(from u in _context.Users
                                   where !_context.Connections.Any(f=>f.FriendId==u.Id && f.UserId==user.Id) && u.Id!=user.Id
                                   select new User
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       Email = u.Email,
                                       ProfileImage = u.ProfileImage,
                                       Title = u.Title
                                   }).ToList();
            List<User> connections=(from u in _context.Users
                                   join con in _context.Connections on u.Id equals con.FriendId
                                    where con.UserId == user.Id && con.IsAccepted==true
                                    select new User
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       Email = u.Email,
                                       ProfileImage = u.ProfileImage,
                                       Title = u.Title,
                                       RequestDate = con.CreatedDate,
                                       IsAccepted = con.IsAccepted
                                   }).ToList();
            ViewData["users"] = newfriends;
            return View(user);
        }

        public IActionResult GetAllConnections()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string json = HttpContext.Session.GetString("CurrentUser");
            User user=JsonSerializer.Deserialize<User>(json);
            List<User> connections = (from u in _context.Users
                                      join con in _context.Connections on u.Id equals con.FriendId
                                      where (con.UserId == user.Id || con.FriendId==user.Id) && con.IsAccepted == true
                                      select new User
                                      {
                                          Id = u.Id,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          Email = u.Email,
                                          ProfileImage = u.ProfileImage,
                                          Title = u.Title,
                                          RequestDate = con.CreatedDate,
                                          IsAccepted = con.IsAccepted
                                      }).ToList();
            List<User> requestSent = (from u in _context.Users
                                      join con in _context.Connections on u.Id equals con.FriendId
                                      where con.UserId == user.Id && con.IsAccepted != true
                                      select new User
                                      {
                                          Id = u.Id,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          Email = u.Email,
                                          ProfileImage = u.ProfileImage,
                                          Title = u.Title,
                                          RequestDate = con.CreatedDate,
                                          IsAccepted = con.IsAccepted
                                      }).ToList();
            List<User> newRequests = (from u in _context.Users
                                      join con in _context.Connections on u.Id equals con.UserId
                                      where con.FriendId == user.Id && con.IsAccepted != true
                                      select new User
                                      {
                                          Id = user.Id,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          Email = u.Email,
                                          ProfileImage = u.ProfileImage,
                                          Title = u.Title,
                                          RequestDate = con.CreatedDate,
                                          IsAccepted = con.IsAccepted,
                                          FriendId=u.Id
                                      }).ToList();
            ViewData["sentRequests"] = requestSent;
            ViewData["newRequests"] = newRequests;
            return View("Connections",connections);
        }
        public IActionResult Jobs()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string json = HttpContext.Session.GetString("CurrentUser");
            User user = JsonSerializer.Deserialize<User>(json);
            var jobs=postService.GetAllJobs().ToList();
            ViewData["personalJobs"]=jobs.Where(x=>x.UserId==user.Id).ToList();
            ViewData["UserId"] = user.Id;
            return View(jobs);
        }
        public IActionResult RemoveJob(Guid Id)
        {
            postService.DeletePost(Id);
            return RedirectToAction("Jobs");
        }
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            string json = HttpContext.Session.GetString("CurrentUser");
            User user = JsonSerializer.Deserialize<User>(json);
            string filename = "";
                if (file.Length > 0)
                {
                    filename = GetUniqueFileName(file.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                    var filePath = Path.Combine(uploads, filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
            }
            user.ProfileImage = "/Uploads/" + filename;
            userService.UpdateUser(user);
            string update = JsonSerializer.Serialize<User>(user);
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.SetString("CurrentUser", update);
            return Json(1);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        [HttpPost]
        public IActionResult PostProfession(string Title)
        {
            string json = HttpContext.Session.GetString("CurrentUser");
            User user = JsonSerializer.Deserialize<User>(json);
            user.Title=Title;
            userService.UpdateUser(user);
            string update = JsonSerializer.Serialize<User>(user);
            HttpContext.Session.Remove("CurrentUser");
            HttpContext.Session.SetString("CurrentUser", update);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SendRequest(Guid UserId,Guid FriendId)
        {
            var exist=connectionService.GetAllConnections().Where(x=>x.UserId==UserId &&  x.FriendId==FriendId).FirstOrDefault();
            if (exist == null)
            {
                Connection con = new Connection()
                {
                    FriendId = FriendId,
                    IsAccepted = false,
                    UserId = UserId
                };
                connectionService.AddConnection(con);
                return Json(1);
            }
            return Json(-1);
        }
        [HttpPost]
        public IActionResult ConfirmRequest(Guid UserId,Guid FriendId)
        {
            var exist=connectionService.GetAllConnections().Where(x=>x.UserId==UserId &&  x.FriendId==FriendId).FirstOrDefault();
            if (exist != null)
            {
                exist.IsAccepted = true;
                connectionService.UpdateConnection(exist);
                return Json(1);
            }
            return Json(-1);
        }
        [HttpPost]
        public IActionResult PostEducation(Education edu)
        {
            educationService.AddEducation(edu);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostExperience(Experience exp)
        {
            experienceService.AddExperience(exp);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostCertificate(LicenseOfCertification certification)
        {
            licenseOfCertificationService.AddLicenseOfCertification(certification);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            postService.AddPost(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostJob(Post post)
        {
            post.IsJob = true;
            postService.AddPost(post);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}