using System.Web.Mvc;

namespace SmartHealthcare.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index or just /
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your comprehensive solution for modern patient and doctor management.";
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with our support team.";
            return View();
        }

        // --- NEW PUBLIC PAGES ---

        // GET: /Home/Feedback
        // Allows users to submit general feedback or read reviews.
        public ActionResult Reviews()
        {
            ViewBag.Title = "Reviews";
            ViewBag.Message = "Tell us about your experience or read what others are saying.";
            return View();
        }

        // GET: /Home/Help
        // Displays the Help/FAQ section.
        public ActionResult Help()
        {
            ViewBag.Title = "Help / FAQ";
            return View();
        }
    }
}