namespace lib_project.Models
{
    public class Book
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Page { get; set; }
        public required bool isExist { get; set; }
        public required int AuthorId { get; set; }
        public required int RackId { get; set; }
        public Rack Rack { get; set; }
        public Author Author { get; set; }

    }
}
