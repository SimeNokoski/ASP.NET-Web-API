using Homework_2.Models;

namespace Homework_2
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book{Id=1,Author = "Author1",Title = "Title1"},
            new Book{Id=2,Author = "Author2",Title = "Title2"},
            new Book{Id=3,Author = "Author3",Title = "Title3"},
        };
    }
}
