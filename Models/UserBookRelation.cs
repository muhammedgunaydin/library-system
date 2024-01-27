namespace lib_project.Models
{
    public class UserBookRelation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BookId { get; set; }
        public virtual ICollection<Book> Book { get; set; }
    }
}
