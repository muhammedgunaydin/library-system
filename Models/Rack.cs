namespace lib_project.Models
{
    public class Rack
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public required int BookCount { get; set; }
        public bool isEmpty => BookCount == Capacity;
    }
}
