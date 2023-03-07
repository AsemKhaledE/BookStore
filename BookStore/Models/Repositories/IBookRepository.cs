 namespace BookStore.Models.Repositories
{
    public interface IBookRepository
    {
        IList<Book> List();
        Book GetOne(int id);
        void Add(Book entity);
        void Update(int id, Book entity);
        void Delete(int id);    
    }
}
