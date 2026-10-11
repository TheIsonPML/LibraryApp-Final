namespace LibraryApp.Core
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Title} - {Author} ({Genre})";
        }
    }
}