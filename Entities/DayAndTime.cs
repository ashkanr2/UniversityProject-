namespace UniversityProject.Entities
{
    public class DayAndTime
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan Time { get; set; }
        public Course Course { get; set; }
    }
}
