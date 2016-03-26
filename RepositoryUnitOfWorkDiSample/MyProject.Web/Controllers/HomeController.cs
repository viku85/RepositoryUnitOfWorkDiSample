using MyProject.Interface.Infrastructure;
using MyProject.Web.Base;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMyProjectUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ActionResult Index()
        {
            MyProjectUnitOfWork.BookRepository.Create(new Model.DataModel.Book
            {
                BookTitle = "C#",
                ISBN = "123",
                PublisherName = "NA"
            });

            if (MyProjectUnitOfWork.BookRepository.Count == 0)
            {
                MyProjectUnitOfWork.Save();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}