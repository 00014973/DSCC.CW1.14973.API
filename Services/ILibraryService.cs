using DSCC.CW1._14973.API.Models;

namespace DSCC.CW1._14973.API.Services
{
    public interface ILibraryService
    {
        // Author Services
        IEnumerable<Author> GetAuthors(); // GET All Authors
        Author GetAuthor(int id); // GET Single Author
        void AddAuthor(Author author); // POST New Author
        void UpdateAuthor(Author author); // PUT Author
        void DeleteAuthor(int id); // DELETE Author

        // Book Services
        IEnumerable<Book> GetBooks(); // GET All Books
        Book GetBook(int id); // Get Single Book
        void AddBook(Book book); // POST New Book
        void UpdateBook(Book book); // PUT Book
        void DeleteBook(int id); // DELETE Book
    }
}
