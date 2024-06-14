using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PracticeForTelerickUI01.Models;
using PracticeForTelerickUI01.Repository;
using PracticeForTelerickUI01.Repository.Interface;
using System.Collections;
using System.Diagnostics;
using System.Text;
using Telerik.SvgIcons;

namespace PracticeForTelerickUI01.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBookRepository bookRepository;
        public HomeController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }




        public IActionResult Index()
        {
            return View();
        }
 


        public IActionResult SearchBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchBook(string bookCode)
        {
            // Perform search using the book service
            var foundBook = bookRepository.SearchBookByCode(bookCode);

            if(foundBook == null)
            {
                ViewBag.SearchResult = "No Book Found";
            }

            return View(foundBook);
        }




        public List<string> GetAllGenres()
        {
            List<string> Data = bookRepository.GetAllGenres();
            return Data;
        }




        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(BookCompleteDataModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.IfAdded = "Invalid Data is Provided.";
                return View(model);
            }

            int result = bookRepository.AddBook(model);

            if (result == -1)
            {
                ViewBag.IfAdded = "Book Code Already Exist.";
                model.BookCode = "";
                return View(model);
            }
            else if (result == 1 && HttpContext.Session.GetString("JsonBookCode") == null)
            {
                ViewBag.IfAdded = "Success";
                ModelState.Clear();
                return View();
            }
            else if (result == 1 && HttpContext.Session.GetString("JsonBookCode") != null)
            {
                TempData["GridDeletionProcess"] = "GridAddSuccess";
                return RedirectToAction("GetAllBooks"); // if update is performed from grid then redirect to grid
            }
            else
            {
                ViewBag.IfAdded = "Procedure is not executed, Please do agian";
                return View(model);
            }
        }




        
        public IActionResult SearchToUpdateBook()
        {
            var message = TempData["SearchResult"] as string;
            if(message != null)
            {
                ViewBag.SearchResult = message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult SearchToUpdateBook(string bookCode)
        {
            var foundBook = bookRepository.SearchBookByCode(bookCode);

            if (foundBook == null)
            {
                ViewBag.SearchResult = "No Book Found";
            }

            return View(foundBook);
        }

        public IActionResult UpdateBook(string bookCode)
        {
            // handling to get view to update book from grid
            string tempBookCode = HttpContext.Session.GetString("JsonBookCode"); // Retrieve bookCode from TempData pass from jsonupdatebook ajax action
            if(tempBookCode != null)
            {
                bookCode = tempBookCode;
            }

            if (bookCode == null && tempBookCode == null)
            {
                TempData["SearchResult"] = "UpdateRedirect";
                return RedirectToAction("SearchToUpdateBook");
            }

            var foundBook = bookRepository.SearchBookByCode(bookCode);

            //ViewData["BookCode"] = bookCode;
            return View(foundBook);
        }

        [HttpPost]
        public IActionResult UpdateBook(BookCompleteDataModel model)
        {
            int updated = bookRepository.UpdateBook(model);
            
            string tempBookCode = HttpContext.Session.GetString("JsonBookCode"); // Retrieve bookCode from TempData pass from jsonupdatebook ajax action
            if(tempBookCode != null)
            {
                TempData["GridDeletionProcess"] = "GridUpdateSuccess";
                return RedirectToAction("GetAllBooks"); // if update is performed from grid then redirect to grid
            }

            if (updated == 1)
            {
                TempData["SearchResult"] = "UpdateSuccess";
                return RedirectToAction("SearchToUpdateBook");
            }
            return View();
        }





        public IActionResult SearchToDeleteBook()
        {
            var message = TempData["SearchResult"] as string;
            if (message != null)
            {
                ViewBag.SearchResult = message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult SearchToDeleteBook(string bookCode)
        {
            var foundBook = bookRepository.SearchBookByCode(bookCode);

            if (foundBook == null)
            {
                ViewBag.SearchResult = "No Book Found";
            }

            return View(foundBook);
        }

        public IActionResult DeleteBook(string bookCode)
        {
            // handling to get view to update book from grid
            string tempBookCode = HttpContext.Session.GetString("JsonBookCode"); // Retrieve bookCode from TempData pass from jsonupdatebook ajax action
            if (tempBookCode != null)
            {
                bookCode = tempBookCode;
            }

            if (bookCode == null && tempBookCode == null)
            {
                TempData["SearchResult"] = "DeleteRedirect";
                return RedirectToAction("SearchToDeleteBook");
            }
            var foundBook = bookRepository.SearchBookByCode(bookCode);
            return View(foundBook);
        }

        [HttpPost]
        public IActionResult DeleteBook(BookCompleteDataModel model)
        {
            int updated = bookRepository.DeleteBook(model.BookCode);

            string tempBookCode = HttpContext.Session.GetString("JsonBookCode"); // Retrieve bookCode from TempData pass from jsonupdatebook ajax action
            if (tempBookCode != null)
            {
                TempData["GridDeletionProcess"] = "GridDeleteSuccess";
                return RedirectToAction("GetAllBooks"); // if deleted from grid then redirect to grid.
            }

            if (updated == 1)
            {
                TempData["SearchResult"] = "DeleteSuccess";
                return RedirectToAction("SearchToDeleteBook");
            }
            return View();
        }






        

        public IActionResult GetAllBooks()
        {
            /*var data = bookRepository.GetAllBooks();*/ // No need dynamically getting data through datasource

            if(HttpContext.Session.GetString("JsonBookCode") != null)
            {
                HttpContext.Session.Remove("JsonBookCode"); // .Clear() clears all session this can be used to remove specific session.
                //HttpContext.Session.SetString("JsonBookCode", null); // .Clear() clears all the sessions defined in appication hence practice to use this set null when one session is used in specific functionality.
            }

            // Just using viewbag in order to maintain notifications on view
            var message = TempData["GridDeletionProcess"] as string;

            if(message == "GridDeleteSuccess")
            {
                ViewBag.GridProcessResult = "DeleteSuccess";
            }
            else if(message == "GridUpdateSuccess")
            {
                ViewBag.GridProcessResult = "UpdateSuccess";
            }
            else if(message == "GridAddSuccess")
            {
                ViewBag.GridProcessResult = "AddSuccess";
            }


            return View();
        }


        public IActionResult GetAllBooksForGrid([DataSourceRequest] DataSourceRequest request)
        {
            var data = bookRepository.GetAllBooks();
            DataSourceResult result = data.ToDataSourceResult(request);
            return Json(result);
        }



        [HttpGet]
        public IActionResult Get_Books_Genres()
        {
            var data = bookRepository.GetAllGenres();
            //DataSourceResult result = data.ToDataSourceResult(request);
            return Json(data);
        }

        [HttpPost]
        public IActionResult ExportTo_ExcelData(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }


        
        public IActionResult JSONAddBook()
        {
            HttpContext.Session.SetString("JsonBookCode", "bookCode");
            return RedirectToAction("AddBook", "Home");
        }

        [HttpPost]
        public IActionResult JSONUpdateBook([FromBody] string bookCode)
        {
            HttpContext.Session.SetString("JsonBookCode", bookCode);
            return Json(new { redirectToUrl = Url.Action("UpdateBook", "Home") });
        }

        [HttpPost]
        public IActionResult JSONDeleteBook([FromBody] string bookCode)
        {
            HttpContext.Session.SetString("JsonBookCode", bookCode);
            return Json(new { redirectToUrl = Url.Action("DeleteBook", "Home") });
        }



        public IActionResult GetVisualizations()
        {
            return View();
        }
        
        public IActionResult GetPerYearBookDistributionData([DataSourceRequest] DataSourceRequest request)
        {
            var yearCount = bookRepository.GetPerYearBookDistribution();
            DataSourceResult result = yearCount.ToDataSourceResult(request);
            return Json(result);
        }

        public IActionResult GetPerGenreBookDistributionData([DataSourceRequest] DataSourceRequest request)
        {
            var genreCount = bookRepository.GetPerGenreBookDistribution();
            DataSourceResult result = genreCount.ToDataSourceResult(request);
            return Json(result);
        }






    }
}
