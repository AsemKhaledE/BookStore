using BookStore.Models;
using BookStore.Models.Repositories;

namespace WebApplication1.Model.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        BookStoreDbContext _context;
        public AuthorRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Add(Author entity)
        {
            _context.Authors.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = GetOne(id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public Author GetOne(int id)
        {
            var author = _context.Authors!.FirstOrDefault(f => f.Id == id);
            return author!;
        }

        public IList<Author> List()
        {
            return _context.Authors!.ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            _context.Update(newAuthor);
            _context.SaveChanges();
        }
    }
}
