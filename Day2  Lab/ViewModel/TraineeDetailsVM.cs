namespace Day2__Lab.ViewModel
{
    public class TraineeDetailsVM
    {
        public int traineID{ get; set; }
        public string traineName { get; set; }
        public string traineAddress { get; set; }
        public string traineDepartment { get; set; }
        public int traineTotalCources { get; set; }
        public int trainePassedCources { get; set; }
        public int traineFailedCources{ get; set; }
        public int traineSucessRate { get; set; }
        public List<TraineeCourcesData> TraineeList { get; set; }
    }
}
