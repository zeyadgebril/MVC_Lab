using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class crsResultRepository : IcrsResultRepository
    {
        CompanyDbcontext db;
        public crsResultRepository(CompanyDbcontext dbcontext)
        {
            db = dbcontext;
        }
        public void Add(crsResult entity)
        {
            db.crsResult.Add(entity);
        }

        public bool Delete(int CourseId, int TraineeId)
        {
            var condition=false;
            var data = GetDataUsingCoursIdandTraineeId(CourseId, TraineeId);
            if (data != null) {
                data.IsDeleted = 1;
                Update(data);
                condition = true;
            }
            return condition;

        }

        public List<crsResult> GetAll(string? include)
        {
            IQueryable<crsResult> query = db.crsResult.Where(cr=>cr.IsDeleted!=1);
            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(",");
                foreach (var item in includes)
                {
                    query = query.Include(item.Trim());
                }

            }
            return query.ToList();
        }

        public crsResult GetById(int id)
        {
           return db.crsResult.Where(cr=>cr.IsDeleted!=1).FirstOrDefault(cr=>cr.Id==id);
        }

        public crsResult GetDataUsingCoursIdandTraineeId(int CourseId, int TraineeId)
        {
            return GetAll("Course,Trainee").Where(cr=>cr.Trainee.IsDeleted!=1&&cr.Course.IsDeleted!=1)
                                             .FirstOrDefault(cr=>cr.Crs_id==CourseId&&cr.Traniee_id==TraineeId);

        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public int TotalTraineePassed(int TraineeId)
        {
            return GetAll("Trainee,Course").Where(cr => cr.degree >= cr.Course.minDegree && cr.Traniee_id == TraineeId && cr.IsDeleted != 1).Count();
        }
        public List<crsResult> TotalTraineeCourses(int TraineeId)
        {
            return  GetAll("Trainee,Course").Where(t => t.Trainee.ID == TraineeId).ToList();
        }

        public void Update(crsResult entity)
        {
            var data = GetById(entity.Id);

            data.degree = entity.degree;
            data.Crs_id = entity.Crs_id;
            data.Traniee_id = entity.Traniee_id;
            data.IsDeleted = entity.IsDeleted;

            db.crsResult.Update(data);
        }
    }
}
