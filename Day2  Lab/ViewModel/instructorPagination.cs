using Day2__Lab.Models;

namespace Day2__Lab.ViewModel
{
    public class instructorPagination
    {
        public List<instructor> instructors { get; set; }
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
