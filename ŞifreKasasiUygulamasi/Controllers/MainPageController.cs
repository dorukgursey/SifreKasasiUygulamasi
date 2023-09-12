using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SifreKasasiUygulamasi.Controllers
{
	public class MainPageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
