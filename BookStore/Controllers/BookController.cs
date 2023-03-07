using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;

        [Obsolete]
        public BookController(IBookRepository bookRepository , IAuthorRepository authorRepository,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            _bookRepository=bookRepository;
            _authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var book = _bookRepository.List();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookRepository.GetOne(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(BookAuthorViewModel model)
        {
            string fileName =string.Empty;

            if (model.File !=null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                fileName = model.File.FileName;
                string fullPath=Path.Combine(uploads, fileName);    
                model.File.CopyTo(target: new FileStream(fullPath,FileMode.Create));
            }


            if (model.AuthorId == -1)
            {
                var vmodel = new BookAuthorViewModel
                {
                    Authors = FillSelectList()
                };
                ViewBag.Message = "Please Select An Author From To List!";     
                return View(vmodel);
            }
            Book book = new Book
            {
                Id = model.BookId,
                Title = model.Title,
                Description = model.Description,
                Author = _authorRepository.GetOne(model.AuthorId),
                ImageUrl=fileName
            };
            try
            {
                _bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _bookRepository.GetOne(id);
            var viewModel = new BookAuthorViewModel
            {
                BookId= book.Id,
                Title = book.Title,
                Description= book.Description,
                AuthorId=book.Author!.Id,
                Authors=_authorRepository.List().ToList()   
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            Book book = new Book
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Author = _authorRepository.GetOne(viewModel.AuthorId)
            };
            try
            {
                _bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _bookRepository.GetOne(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                _bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       private List<Author> FillSelectList()
        {
            var authors=_authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please Select An Author ---" });
            return authors;
        }
    }
}
