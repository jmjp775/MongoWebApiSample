using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApiNewer.Models;
using MongoDB.Driver;

namespace BooksApiNewer.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(Book => true).ToList();

        //Returns all documents in the collection matching the provided search criteria.
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        //Inserts the provided object as a new document in the collection.
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        //Replaces the single document matching the provided search criteria with the provided object.
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        //Deletes a single document matching the provided search criteria.
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
