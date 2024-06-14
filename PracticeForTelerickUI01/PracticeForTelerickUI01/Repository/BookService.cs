using PracticeForTelerickUI01.Data;
using PracticeForTelerickUI01.Models;
using PracticeForTelerickUI01.Repository.EFCore;
using PracticeForTelerickUI01.Repository.Interface;

namespace PracticeForTelerickUI01.Repository
{
    public class BookService : IBookRepository
    {
        BookDbContext dbcontext;
        public BookService(BookDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public BookCompleteDataModel SearchBookByCode(string code)
        {
            var data = (from book in dbcontext.Books
                        join bookInfo in dbcontext.BooksInformation on book.BookCode equals bookInfo.BookCode
                        where book.BookCode == code
                        select new BookCompleteDataModel
                        {
                            BookId = book.BookId,
                            BookCode = book.BookCode,
                            BookName = book.BookName,
                            AddedOnDate = DateTime.ParseExact(book.AddedDate, "dd-MM-yyyy HH:mm:ss", null),
                            Genre = book.BookType,
                            AuthorName = bookInfo.Author,
                            TotalStock = bookInfo.Stock,
                            BorrowedCount = bookInfo.BorrowedCount,
                        }).FirstOrDefault();
            return data;
        }



        public List<string> GetAllGenres()
        {
            var bookGenres = new List<string> { "Fantasy", "Science Fiction", "Mystery", "Thriller", "Romance", "Historical Fiction", "Horror", "Biography", "Self-Help" };
            return bookGenres;
        }



        public int AddBook(BookCompleteDataModel bookDetails)
        {
            var bookExist = this.SearchBookByCode(bookDetails.BookCode);
            if (bookExist != null)
            {
                return -1;
            }

            Book book = new()
            {
                BookCode = bookDetails.BookCode,
                BookName = bookDetails.BookName,
                AddedDate = bookDetails.AddedOnDate.ToString("dd-MM-yyyy HH:mm:ss"),
                BookType = bookDetails.Genre,
            };

            BookInformation bookInfo = new()
            {
                BookCode = bookDetails.BookCode,
                Author = bookDetails.AuthorName,
                Stock = bookDetails.TotalStock,
                BorrowedCount = bookDetails.BorrowedCount,
            };


            dbcontext.Books.Add(book);
            dbcontext.BooksInformation.Add(bookInfo);

            int row = dbcontext.SaveChanges();
            if (row < 1)
            {
                return 0;
            }
            return 1;
        }



        public int UpdateBook(BookCompleteDataModel book)
        {

            var bookData = dbcontext.Books.FirstOrDefault(m => m.BookCode == book.BookCode);
            var bookInfoData = dbcontext.BooksInformation.FirstOrDefault(m => m.BookCode == book.BookCode);

            bookData.BookCode = book.BookCode;
            bookData.BookName = book.BookName;
            bookData.AddedDate = book.AddedOnDate.ToString("dd-MM-yyyy HH:mm:ss");
            bookData.BookType = book.Genre;

            bookInfoData.BookCode = book.BookCode;
            bookInfoData.Author = book.AuthorName;
            bookInfoData.Stock = book.TotalStock;
            bookInfoData.BorrowedCount = book.BorrowedCount;

            int row = dbcontext.SaveChanges();
            if (row < 1)
            {
                return 0;
            }
            return 1;
        }




        public int DeleteBook(string bookCode)
        {

            var bookData = dbcontext.Books.FirstOrDefault(m => m.BookCode == bookCode);
            var bookInfoData = dbcontext.BooksInformation.FirstOrDefault(m => m.BookCode == bookCode);

            dbcontext.Books.Remove(bookData);
            dbcontext.BooksInformation.Remove(bookInfoData);

            int row = dbcontext.SaveChanges();
            if (row < 1)
            {
                return 0;
            }
            return 1;
        }




        public IEnumerable<BookCompleteDataModel> GetAllBooks()
        {
            var data = (from book in dbcontext.Books
                        join bookInfo in dbcontext.BooksInformation on book.BookCode equals bookInfo.BookCode
                        select new BookCompleteDataModel
                        {
                            BookId = book.BookId,
                            BookCode = book.BookCode,
                            BookName = book.BookName,
                            AddedOnDate = DateTime.ParseExact(book.AddedDate, "dd-MM-yyyy HH:mm:ss", null),
                            Genre = book.BookType,
                            TotalStock = bookInfo.Stock,
                            AuthorName = bookInfo.Author,
                            BorrowedCount = bookInfo.BorrowedCount,
                        }).ToList();
            return data;
        }




        public IEnumerable<PerCategoryBookDistributionModel> GetPerYearBookDistribution()
        {
            var data = (from book in dbcontext.Books
                        select DateTime.ParseExact(book.AddedDate, "dd-MM-yyyy HH:mm:ss", null).Year).ToList();

            var yearFrequencyList = data.GroupBy(year => year)
                            .Select(group => new { Year = group.Key, Count = group.Count() })
                            .ToList();

            List<PerCategoryBookDistributionModel> yearDistribution = new List<PerCategoryBookDistributionModel>();

            int totalDistribution = 0;
            foreach (var item in yearFrequencyList)
            {
                PerCategoryBookDistributionModel newItem = new PerCategoryBookDistributionModel();
                newItem.Category = item.Year.ToString();
                newItem.Distribution = item.Count;

                totalDistribution = totalDistribution + item.Count;
                yearDistribution.Add(newItem);
            }

            foreach (var item in yearDistribution)
            {
                item.Distribution = (item.Distribution * 100.0f) / totalDistribution;
            }

            return yearDistribution;

        }


        public IEnumerable<PerCategoryBookDistributionModel> GetPerGenreBookDistribution()
        {
            var data = (from book in dbcontext.Books
                        select book.BookType).ToList();

            var genreFrequencyList = data.GroupBy(booktype => booktype)
                            .Select(group => new { BookType = group.Key, Count = group.Count() })
                            .ToList();

            List<PerCategoryBookDistributionModel> genreDistribution = new List<PerCategoryBookDistributionModel>();

            int totalDistribution = 0;
            foreach (var item in genreFrequencyList)
            {
                PerCategoryBookDistributionModel newItem = new PerCategoryBookDistributionModel();
                newItem.Category = item.BookType;
                newItem.Distribution = item.Count;

                totalDistribution = totalDistribution + item.Count;
                genreDistribution.Add(newItem);
            }

            foreach (var item in genreDistribution)
            {
                item.Distribution = (item.Distribution * 100.0f) / totalDistribution;
            }

            return genreDistribution;
        }

    }
}
