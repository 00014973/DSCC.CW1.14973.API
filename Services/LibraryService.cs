using Microsoft.EntityFrameworkCore;
using DSCC.CW1._14973.API.Data;
using DSCC.CW1._14973.API.Models;

namespace DSCC.CW1._14973.API.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;

        public LibraryService(AppDbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        #region Authors
        public IEnumerable<Author> GetAuthors()
        {
            return _db.Authors.ToList();
        }

        public Author GetAuthor(int id)
        {

            return _db.Authors.Find(id);
        }

        public void AddAuthor(Author author)
        {
            _db.Add(author);
            Save();
        }


        public void UpdateAuthor(Author author)
        {

            _db.Entry(author).State = EntityState.Modified;
            Save();
        }

        public void DeleteAuthor(int id)
        {
            var author = _db.Authors.Find(id);
            _db.Authors.Remove(author);

            Save();

        }

        #endregion Authors

        #region Books

        public IEnumerable<Book> GetBooks()
        {
            return _db.Books.ToList();

        }

        public Book GetBook(int id)
        {
            var book = _db.Books.Find(id);
           
            return book;

        }

        public void AddBook(Book book)
        {
            _db.Add(book);
            Save();
        }

        public void UpdateBook(Book book)
        {
             _db.Entry(book).State = EntityState.Modified;
            Save();
        }

        public void DeleteBook(int id)
        {

            var book = _db.Books.Find(id);

            _db.Books.Remove(book);
            Save();
        }
        #endregion Books
    }
}

