namespace Day2__Lab.ViewModel
{
    public class CourseFinalResultVm
    {
        public string CouseName { get; set; }
        public int TotalStudent { get; set; }
        public int TotalPassed { get; set; }
        public int TotalFaild { get; set; }
        public int PassRate { get; set; }
        public List<CourseResultVM> TraneeiList { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
