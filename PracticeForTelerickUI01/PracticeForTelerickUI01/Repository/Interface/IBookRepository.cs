using PracticeForTelerickUI01.Models;

namespace PracticeForTelerickUI01.Repository.Interface
{
    public interface IBookRepository
    {
        public BookCompleteDataModel SearchBookByCode(string code);
        public List<string> GetAllGenres();
        public int AddBook(BookCompleteDataModel book);
        public int UpdateBook(BookCompleteDataModel book);
        public int DeleteBook(string book);
        public IEnumerable<BookCompleteDataModel> GetAllBooks();

        public IEnumerable<PerCategoryBookDistributionModel> GetPerYearBookDistribution();
        public IEnumerable<PerCategoryBookDistributionModel> GetPerGenreBookDistribution();
    }
}
