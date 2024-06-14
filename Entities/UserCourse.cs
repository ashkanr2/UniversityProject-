namespace UniversityProject.Entities
{
    public class UserCourse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Course course { get; set; }
        public User user { get; set; }

    }
}
