using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Interface.Infrastructure;
using MyProject.Web.Base;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyProject.Web.Controllers
{
    public class HomeController
        : BaseController
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
    }
}