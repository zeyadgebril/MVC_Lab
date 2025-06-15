namespace Day2__Lab.ViewModel
{
    public class TraineeCourcesData
    {
        public int CourceID { get; set; }
        public string CourceName { get; set; }
        public int CourceGrade { get; set; }
        public int CourceDegree { get; set; }
        public int CoursMinDegree { get; set; }

        public int CoursePersentageGrade { get; set; }
        public string CourceStatus { get; set; }
        public string Color { get; set; }
        public string CourceInstructor { get; set; }
        public TraineNameAndCourseVM TNC { get; set; }
    }
}
