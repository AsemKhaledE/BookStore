namespace BookStore.Models.Repositories
{
    public interface IAuthorRepository
    {
        IList<Author> List();
        Author GetOne(int id);
        void Add(Author entity);
        void Update(int id, Author entity);
        void Delete(int id);
    }
}
