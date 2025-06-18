using Day2__Lab.Models;

namespace Day2__Lab.Repository
{
    public interface IcrsResultRepository : IRepository<crsResult>
    {

        public bool Delete(int CourseId,int TraineeId);
        public crsResult GetDataUsingCoursIdandTraineeId(int CourseId, int TraineeId);
        public int TotalTraineePassed(int TraineeId);
        public List<crsResult> TotalTraineeCourses(int TraineeId);

    }
}
