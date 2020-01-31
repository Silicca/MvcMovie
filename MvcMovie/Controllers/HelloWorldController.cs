using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
	public class HelloWorldController : Controller
	{
		// 
		// call the method of controller's view

		public ActionResult Index()
		{
			return View();
		}

		// 
		// GET: /HelloWorld/Welcome?parameter1&parameter2 

		public ActionResult Welcome(string name, int ID = 1)
		{
			ViewBag.Message = "Hello " + name;
			ViewBag.ID = ID;

			return View();
		}
	}
}