﻿namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class OverviewController : Controller
    {
        #region Ctor & DI

        public OverviewController()
        {

        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}