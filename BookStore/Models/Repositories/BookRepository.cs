using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model.Repositories
{
    public class BookRepository : IBookRepository
    {
        BookStoreDbContext _context;
        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Add(Book entity)
        {
            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetOne(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public Book GetOne(int id)
        {
            var book = _context.Books?.Include(a => a.Author).FirstOrDefault(f => f.Id == id);
            return book!;
        }

        public IList<Book> List()
        {
            return _context.Books!.Include(a=>a.Author).ToList(); ;
        }

        public void Update(int id, Book newBook)
        {
            _context.Update(newBook);
            _context.SaveChanges();
        }
        //public List<Book> Search(string term)
        //{
        //    var books = _context.Books.Include(a => a.Author)
        //        .Where(b => b.Title!.Contains(term)
        //                || b.Description!.Contains(term)
        //              8    ||b.Author.FullName.Contains(term)).ToList();


        //    return result;
            
        //}
    }
}
